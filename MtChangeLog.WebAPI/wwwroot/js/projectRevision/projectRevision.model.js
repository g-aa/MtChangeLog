class ProjectRevision{
    constructor(){
        this.url = entitiesRepository.getProjectsRevisionsUrl();
        this.editable = {};
        this.authors = [];
        this.armEdits = [];
        this.projectVersions = [];
        this.algorithms = [];
        this.parentRevisions = [];
        this.communications = [];

        this.configure = async function(){
            let urlAuthors = entitiesRepository.getAuthorsUrl();
            this.authors = await entitiesRepository.getEntitiesInfo(urlAuthors);
        
            let urlArms = entitiesRepository.getArmEditsUrl();
            this.armEdits = await entitiesRepository.getEntitiesInfo(urlArms);
        
            let urlVersions = entitiesRepository.getProjectsVersionsUrl();
            this.projectVersions = await entitiesRepository.getEntitiesInfo(urlVersions);

            let urlParent = entitiesRepository.getParentProjectsRevisionsUrl();
            this.parentRevisions = await entitiesRepository.getEntitiesInfo(urlParent);

            let urlAlgorithms = entitiesRepository.getRelayAlgorithmsUrl();
            this.algorithms = await entitiesRepository.getEntitiesInfo(urlAlgorithms);

            let urlCommunications = entitiesRepository.getCommunicationsUrl();
            this.communications = await entitiesRepository.getEntitiesInfo(urlCommunications);
        }
    }

    // получить armEdit по умолчанию:
    async defaultInitialize(){
    } 

    // получить конкретную ревизию БФПО из bd:
    async initialize(entityInfo){
        this.editable = await entitiesRepository.getEntityDetails(this.url, entityInfo);
        
        await this.configure();

        // отправить данные:
        this.submit = async function(){
            let answer = await entitiesRepository.updateEntity(this.url, this.editable);
            if(typeof(this.beforeEnding) === "function"){
                await this.beforeEnding(this.url, answer);
            }
        };
    }

    // получение шаблона для конкретной редакции:
    async byVersionInitialize(versionInfo){
        let urlTemplate = entitiesRepository.getProjectsRevisionsTemplateUrl();
        this.editable = await entitiesRepository.getEntityDetails(urlTemplate, versionInfo);

        await this.configure();

        // отправить данные:
        this.submit = async function(){
            let answer = await entitiesRepository.createEntity(this.url, this.editable);
            if(typeof(this.beforeEnding) === "function"){
                await this.beforeEnding(this.url, answer);
            }
        };
    }

    //
    async beforeEnding(url, answer){

    }

    getAllArmEdits(){
        return this.armEdits;
    }

    getArmEdit(){
        return this.editable.armEdit;
    }

    setArmEdit(id = ""){
        if (id == undefined || id == null || id == ""){
            throw new Error("не указан ArmEdit id!");
        }
        this.editable.armEdit = this.armEdits.find(function(item){
            return item.id == id;
        });
    }

    getAllAuthors(){
        return this.authors;
    }

    getAuthors(){
        return this.editable.authors;
    }

    setAuthors(ids = [""]){
        if (ids == undefined || ids == null){
            throw new Error("не указан перечень authors id!");
        }
        this.editable.authors = this.authors.filter(function(item){
            return ids.indexOf(item.id) != -1 
        });
    }

    getAllAlgorithms(){
        return this.algorithms;
    }

    getAlgorithms(){
        return this.editable.relayAlgorithms;
    }

    setAlgorithms(ids = [""]){
        if (ids == undefined || ids == null){
            throw new Error("не указан перечень algorithms id!");
        }
        this.editable.relayAlgorithms = this.algorithms.filter(function(item){
            return ids.indexOf(item.id) != -1 
        });
    }

    getAllCommunications(){
        return this.communications;
    }

    getCommunication(){
        return this.editable.communication;
    }

    setCommunication(id = ""){
        if (id == undefined || id == null || id == ""){
            throw new Error("не указан communication id!");
        }
        this.editable.communication = this.communications.find(function(item){
            return item.id == id;
        });
    }

    getDate(){
        return this.editable.date;
    }

    setDate(newDate = new Date()){
        function pad(number){
            if (number < 10){
              return '0' + number;
            }
            return number;
        }
        this.editable.date =  newDate.getFullYear() + '-' + pad(newDate.getMonth() + 1) + '-' + pad(newDate.getDate()) + " 00:00:00";
    }

    getAllParentRevisions(){
        return this.parentRevisions;
    }

    getParentRevision(){
        return this.editable.parentRevision;
    }

    setParentRevision(id = ""){
        if (id == undefined || id == null || id == ""){
            throw new Error("не указан parent project revision id!");
        }
        this.editable.parentRevision = this.parentRevisions.find(function(item){
            return item.id == id;
        });
    }

    getAllProjectVersions(){
        return this.projectVersions;
    }

    getProjectVersion(){
        return this.editable.projectVersion;
    }

    setProjectVersion(id = ""){
        if (id == undefined || id == null || id == ""){
            throw new Error("не указан project version id!");
        }
        this.editable.projectVersion = this.projectVersions.find(function(item){
            return item.id == id;
        });
    }

    getRevision(){
        return this.editable.revision;
    }

    setRevision(newRevision = ""){
        this.editable.revision = newRevision;
    }

    getReason(){
        return this.editable.reason;
    }

    setReason(newReason = ""){
        this.editable.reason = newReason;
    }

    getDescription(){
        return this.editable.description;
    }

    setDescription(newDesc = ""){
        this.editable.description = newDesc;
    }
}