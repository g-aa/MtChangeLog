class ProjectRevision {
    constructor() {
        
        this.url = entitiesRepository.getProjectsRevisionsUrl();
        this.editable = { };
        this.editFunc = null;
    }

    // получить armEdit по умолчанию:
    async defaultInitialize(){

    } 

    // получить конкретный armEdit из bd:
    async initialize(entityInfo){

    }

    //
    async beforeEnding(url, answer){

    }

    getProjectVersion(){
        return this.editable.projectVersion;
    }

    setProjectVersion(newValue = {}){
        this.editable.projectVersion = newValue;
    }

    getParentRevision(){

    }

    getArmEdit(){
        return this.editable.armEdit;
    }

    setArmEdit(newValue = {}){
        this.editable.armEdit = newValue;
    }
}