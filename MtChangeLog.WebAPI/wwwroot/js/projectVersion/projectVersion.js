class ProjectVersion {
    constructor() {
        this.url = entitiesRepository.getProjectsVersionsUrl();
        this.editable = null;
        this.statuses = [];
        this.platforms = [];
        this.analogModules = [];
        this.editFunc = null;
    }

    // получить версию проекта по умолчанию:
    async defaultInitialize() {
        this.editable = await entitiesRepository.getDefaultEntity(this.url);

        this.statuses = await entitiesRepository.getProjectStatuses();
        
        let urlPlatform = entitiesRepository.getPlatformsUrl();
        this.platforms = await entitiesRepository.getEntitiesInfo(urlPlatform);

        // отправить данные:
        this.submit = async function() {
            let answer = await entitiesRepository.createEntity(this.url, this.editable);
            await beforeEnding(this.url, answer);
        };
    }

    // получить конкретную версию проекта из db:
    async initialize(entityInfo) {
        this.editable = await entitiesRepository.getEntityDetails(this.url, entityInfo);

        this.statuses = await entitiesRepository.getProjectStatuses();

        let urlPlatform = entitiesRepository.getPlatformsUrl();
        this.platforms = await entitiesRepository.getEntitiesInfo(urlPlatform);

        let platform = await entitiesRepository.getEntityDetails(urlPlatform, this.editable.platform);
        this.analogModules = platform.analogModules;

        // отправить данные:
        this.submit = async function() {
            let answer = await entitiesRepository.updateEntity(this.url, this.editable);
            await beforeEnding(this.url, answer);
        };
    }

    async beforeEnding(url, answer) {

    }

    getDivg() {
        return this.editable.divg;
    }

    setDivg(newDivg = "") {
        this.editable.divg = newDivg;
    }

    getTitle() {
        return this.editable.title;
    }

    setTitle(newTitle = "") {
        this.editable.title = newTitle;
    }

    getVersion() {
        return this.editable.version;
    }

    setVersion(newVersion = "") {
        this.editable.version = newVersion;
    }

    getStatus() {
        return this.editable.status;
    }

    getStatuses() {
        return this.statuses;
    }

    setStatus(newStatus = "") {
        this.editable.status = newStatus;
    }

    getPlatform(){
        if(this.editable.platform != undefined && this.editable.platform != null){
            return this.editable.platform.id;
        } else{
            return "";
        }
    }

    getPlatforms() {
        return this.platforms;
    }
    
    async setPlatform(id = ""){
        if (id == undefined || id == null || id == ""){
            throw new Error("что-то пошло не так !!!");
        }
        
        let urlPlatform = entitiesRepository.getPlatformsUrl();
        let platform = await entitiesRepository.getEntityDetails(urlPlatform, { id:id });
            
        this.editable.platform = this.platforms.find(
            function(item, index, array){
                return item.id == id;
            });
        this.analogModules = platform.analogModules;
    }

    getAnalogModule(){
        if(this.editable.analogModule != undefined && this.editable.analogModule != null){
            return this.editable.analogModule.id;
        } else{
            return "";
        }
    }

    getAnalogModules(){
        return this.analogModules;
    }

    setAnalogModule(id = ""){
        this.editable.analogModule = this.analogModules.find(
            function(item, index, array){
                return item.id == id;
            }
        );
    }

    getDescription(){
        return this.editable.description;
    }

    setDescription(newDesc = ""){
        this.editable.description = newDesc;
    }
}