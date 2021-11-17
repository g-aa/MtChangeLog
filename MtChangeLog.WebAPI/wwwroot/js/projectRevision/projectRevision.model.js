class ProjectRevision{
    constructor(){
        this.editable = {};
        this.authors = [];
        this.armEdits = [];
        this.projectVersions = [];
        this.algorithms = [];
        this.parentRevisions = [];
        this.communications = [];
        this.configure = async function(){
            this.authors = await repository.getSortAuthors();
            this.armEdits = await repository.getSortArmEdits();
            this.algorithms = await repository.getSortRelayAlgorithms();
            this.communications = await repository.getSortCommunications();
            this.projectVersions = await repository.getSortProjectVersions();
            this.parentRevisions = await repository.getSortProjectRevisions();
        }
    }

    // получение шаблона для конкретной редакции:
    async defaultInitialize(versionInfo){
        // получить шаблон:
        this.editable = await repository.getTemplateProjectRevison(versionInfo);
        await this.configure();

        // отправить данные:
        this.submit = async function(){
            let answer = await repository.createProjectRevison(this.editable);
            if(typeof(this.beforeEnding) === "function"){
                await this.beforeEnding(answer);
            }
        };
    }

    // получить конкретную ревизию БФПО из bd:
    async initialize(entityInfo){
        // получить из БД:
        this.editable = await repository.getProjectRevisonDetails(entityInfo);
        await this.configure();

        // отправить данные:
        this.submit = async function(){
            let answer = await repository.updateProjectRevison(this.editable);
            if(typeof(this.beforeEnding) === "function"){
                await this.beforeEnding(answer);
            }
        };
    }

    //
    async beforeEnding(answer){

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