class ProjectVersion{
    constructor(){
        this.editable = {};
        this.statuses = [];
        this.platforms = [];
        this.analogModules = [];
        this.configure = async function(){
            this.statuses = await repository.getProjectVersionStatuses();
            this.platforms = await repository.getSortPlatforms();
            //let platform = await repository.getPlatformDetails(this.editable.platform);
            //this.analogModules = platform.analogModules;
        }
    }

    // получить версию проекта по умолчанию:
    async defaultInitialize(){
        // получить шаблон:
        this.editable = await repository.getProjectVersionTemplate();
        await this.configure();
        
        // отправить данные:
        this.submit = async function(){
            let answer = await repository.createProjectVersion(this.editable);
            if(typeof(this.beforeEnding) === "function"){
                await this.beforeEnding(answer);
            }
        };
    }

    // получить конкретную версию проекта из db:
    async initialize(entityInfo){
        // получить из БД:
        this.editable = await repository.getProjectVersionDetails(entityInfo);
        await this.configure();

        let platform = await repository.getPlatformDetails(this.editable.platform);
        this.analogModules = platform.analogModules;

        // отправить данные:
        this.submit = async function(){
            let answer = await entitiesRepository.updateEntity(this.editable);
            if(typeof(this.beforeEnding) === "function"){
                await this.beforeEnding(answer);
            }
        };
    }

    async beforeEnding(answer) {

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
        let platform = await repository.getPlatformDetails({ id:id });
        this.analogModules = platform.analogModules;
        this.editable.platform = this.platforms.find(function(item, index, array){
            return item.id == id;
        });
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