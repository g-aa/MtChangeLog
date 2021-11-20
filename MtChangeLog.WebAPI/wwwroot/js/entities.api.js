class EntitiesRepository{
    constructor(){

    }

    // CRUD API:
    async getEntitiesInfo(urlString){
        let response = await fetch(urlString, {
            method:"GET",
            headers:{ 
                "Accept":"application/json" 
            }
        });
        let answer = await response.json();
        if (response.ok) {
            return answer;
        } else{
            throw new Error("HTTP GET error<br>" + response.status + " - " + response.statusText + "<br>" + answer);
        }
    }
    async getEntityDetails(urlString, module = {}){
        let response = await fetch(urlString + "/" + module.id, {
            method:"GET",
            headers:{ 
                "Accept":"application/json" 
            }
        });
        let answer = await response.json();
        if (response.ok){
            return answer;
        } else{
            throw new Error("HTTP GET error<br>" + response.status + " - " + response.statusText + "<br>" + answer);
        }
    }
    async createEntity(urlString, module = {}){
        let response = await fetch(urlString, {
            method:"POST",
            headers:{ 
                "Accept":"application/json", 
                "Content-Type":"application/json" 
            },
            body:JSON.stringify(module)
        });
        let answer = await response.json();
        if (response.ok){
            return answer;
        } else{
            throw new Error("HTTP POST error<br>" + response.status + " - " + response.statusText + "<br>" + answer);
        }
    }
    async updateEntity(urlString, module = {}){
        let response = await fetch(urlString + "/" + module.id, {
            method:"PUT",
            headers:{
                "Accept":"application/json", 
                "Content-Type":"application/json" 
            },
            body:JSON.stringify(module)
        });
        let answer = await response.json();
        if (response.ok){
            return answer;
        } else{
            throw new Error("HTTP PUT error<br>" + response.status + " - " + response.statusText + "<br>" + answer);
        }
    }
    async deleteEntity(urlString, module = {}){
        let response = await fetch(urlString + "/" + module.id, {
            method:"DELETE",
            headers:{ 
                "Accept":"application/json" 
            }
        });
        let answer = await response.json();
        if (response.ok){
            return answer;
        } else{
            throw new Error("HTTP DELETE error<br>" + response.status + " - " + response.statusText + "<br>" + answer);
        }
    }

    // Аналоговые модули:
    async getSortAnalogModules(){
        return await this.getEntitiesInfo("/api/AnalogModules/ShortViews");
    }
    async getTableAnalogModules(){
        return await this.getEntitiesInfo("/api/AnalogModules/TableViews");
    }
    async getAnalogModuleTemplate(){
        return await this.getEntitiesInfo("/api/AnalogModules/Template");
    }
    async getAnalogModuleDetails(entityInfo = {}){
        return await this.getEntityDetails("/api/AnalogModules", entityInfo);
    }
    async createAnalogModule(entity = {}){
        return await this.createEntity("/api/AnalogModules", entity);
    }
    async updateAnalogModule(entity = {}){
        return await this.updateEntity("/api/AnalogModules", entity);
    }
    async deleteAnalogModule(entity = {}){
        return await this.deleteEntity("/api/AnalogModules", entity);
    }

    // Платформы:
    async getSortPlatforms(){
        return await this.getEntitiesInfo("/api/Platforms/ShortViews");
    }
    async getTablePlatforms(){
        return await this.getEntitiesInfo("/api/Platforms/TableViews");
    }
    async getPlatformTemplate(){
        return await this.getEntitiesInfo("/api/Platforms/Template");
    }
    async getPlatformDetails(entityInfo = {}){
        return await this.getEntityDetails("/api/Platforms", entityInfo);
    }
    async createPlatform(entity = {}){
        return await this.createEntity("/api/Platforms", entity);
    }
    async updatePlatform(entity = {}){
        return await this.updateEntity("/api/Platforms", entity);
    }
    async deletePlatform(entity = {}){
        return await this.deleteEntity("/api/Platforms", entity);
    }

    // ArmEdits:
    async getSortArmEdits(){
        return await this.getEntitiesInfo("/api/ArmEdits/ShortViews");
    }
    async getTableArmEdits(){
        return await this.getEntitiesInfo("/api/ArmEdits/TableViews");
    }
    async getArmEditTemplate(){
        return await this.getEntitiesInfo("/api/ArmEdits/Template");
    }
    async getArmEditDetails(entityInfo = {}){
        return await this.getEntityDetails("/api/ArmEdits", entityInfo);
    }
    async createArmEdit(entity = {}){
        return await this.createEntity("/api/ArmEdits", entity);
    }
    async updateArmEdit(entity = {}){
        return await this.updateEntity("/api/ArmEdits", entity);
    }
    async deleteArmEdit(entity = {}){
        return await this.deleteEntity("/api/ArmEdits", entity);
    }

    // Авторы:
    async getSortAuthors(){
        return await this.getEntitiesInfo("/api/Authors/ShortViews");
    }
    async getTableAuthors(){
        return await this.getEntitiesInfo("/api/Authors/TableViews");
    }
    async getAuthorTemplate(){
        return await this.getEntitiesInfo("/api/Authors/Template");
    }
    async getAuthorDetails(entityInfo = {}){
        return await this.getEntityDetails("/api/Authors", entityInfo);
    }
    async createAuthor(entity = {}){
        return await this.createEntity("/api/Authors", entity);
    }
    async updateAuthor(entity = {}){
        return await this.updateEntity("/api/Authors", entity);
    }
    async deleteAuthor(entity = {}){
        return await this.deleteEntity("/api/Authors", entity);
    }

    // коммуникации:
    async getSortCommunications(){
        return await this.getEntitiesInfo("/api/Communications/ShortViews");
    }
    async getTableCommunications(){
        return await this.getEntitiesInfo("/api/Communications/TableViews");
    }
    async getCommunicationTemplate(){
        return await this.getEntitiesInfo("/api/Communications/Template");
    }
    async getCommunicationDetails(entityInfo = {}){
        return await this.getEntityDetails("/api/Communications", entityInfo);
    }
    async createCommunication(entity = {}){
        return await this.createEntity("/api/Communications", entity);
    }
    async updateCommunication(entity = {}){
        return await this.updateEntity("/api/Communications", entity);
    }
    async deleteCommunication(entity = {}){
        return await this.deleteEntity("/api/Communications", entity);
    }

    // Релейные алгоритмы:
    async getSortRelayAlgorithms(){
        return await this.getEntitiesInfo("/api/RelayAlgorithms/ShortViews");
    }
    async getTableRelayAlgorithms(){
        return await this.getEntitiesInfo("/api/RelayAlgorithms/TableViews");
    }
    async getRelayAlgorithmTemplate(){
        return await this.getEntitiesInfo("/api/RelayAlgorithms/Template");
    }
    async getRelayAlgorithmDetails(entityInfo = {}){
        return await this.getEntityDetails("/api/RelayAlgorithms", entityInfo);
    }
    async createRelayAlgorithm(entity = {}){
        return await this.createEntity("/api/RelayAlgorithms", entity);
    }
    async updateRelayAlgorithm(entity = {}){
        return await this.updateEntity("/api/RelayAlgorithms", entity);
    }
    async deleteRelayAlgorithm(entity = {}){
        return await this.deleteEntity("/api/RelayAlgorithms", entity);
    }

    // Версии проектов:
    async getSortProjectVersions(){
        return await this.getEntitiesInfo("/api/ProjectVersions/ShortViews");
    }
    async getTableProjectVersions(){
        return await this.getEntitiesInfo("/api/ProjectVersions/TableViews");
    }
    async getProjectVersionTemplate(){
        return await this.getEntitiesInfo("/api/ProjectVersions/Template");
    }
    async getProjectVersionStatuses() {
        return await this.getEntitiesInfo("/api/ProjectVersions/Statuses");
    }
    async getProjectVersionDetails(entityInfo = {}){
        return await this.getEntityDetails("/api/ProjectVersions", entityInfo);
    }
    async createProjectVersion(entity = {}){
        return await this.createEntity("/api/ProjectVersions", entity);
    }
    async updateProjectVersion(entity = {}){
        return await this.updateEntity("/api/ProjectVersions", entity);
    }
    async deleteProjectVersion(entity = {}){
        return await this.deleteEntity("/api/ProjectVersions", entity);
    }

    // Редакция проекта:
    async getSortProjectRevisions(){
        return await this.getEntitiesInfo("/api/ProjectRevisions/ShortViews");
    }
    async getTableProjectRevisions(){
        return await this.getEntitiesInfo("/api/ProjectRevisions/TableViews");
    }
    async getTemplateProjectRevison(projectVersion = {}){
        return await this.getEntityDetails("/api/ProjectRevisions/Template", projectVersion);
    }
    async getProjectRevisonDetails(entityInfo = {}){
        return await this.getEntityDetails("/api/ProjectRevisions", entityInfo);
    }
    async createProjectRevison(entity = {}){
        return await this.createEntity("/api/ProjectRevisions", entity);
    }
    async updateProjectRevison(entity = {}){
        return await this.updateEntity("/api/ProjectRevisions", entity);
    }
    async deleteProjectRevison(entity = {}){
        return await this.deleteEntity("/api/ProjectRevisions", entity);
    }

    // Дерево версий:
    async getProjectTreeTitle(){
        return await this.getEntitiesInfo("/api/ProjectTrees");
    }
    async getProjectTree(title = ""){
        if(title == ""){
            throw Error("не указано наименование проекта");
        }
        return await this.getEntityDetails("/api/ProjectTrees", { id:title });
    }

    // История проекта:
    async getProjectHistoryTitles(){
        return await this.getEntitiesInfo("/api/ProjectHistory");
    }
    async getProjectHistory(entityId = {}){
        return await this.getEntityDetails("/api/ProjectHistory", { id:entityId });
    }
}

if(repository == undefined || repository == null){
    var repository = new EntitiesRepository();
}