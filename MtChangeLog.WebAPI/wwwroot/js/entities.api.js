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
        //return await this.getEntitiesInfo("/api/AnalogModules/ShortViews");
        return await this.getEntitiesInfo("/api/AnalogModules");
    }
    async getTableAnalogModules(){
        //return await this.getEntitiesInfo("/api/AnalogModules/TableViews");
        return await this.getEntitiesInfo("/api/AnalogModules");
    }
    async getAnalogModuleTemplate(){
        //return await this.getEntitiesInfo("/api/AnalogModules/Template");
        return await this.getEntitiesInfo("/api/AnalogModules/default");
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
        //return await this.getEntitiesInfo("/api/Platforms/ShortViews");
        return await this.getEntitiesInfo("/api/Platforms");
    }
    async getTablePlatforms(){
        //return await this.getEntitiesInfo("/api/Platforms/TableViews");
        return await this.getEntitiesInfo("/api/Platforms");
    }
    async getPlatformTemplate(){
        //return await this.getEntitiesInfo("/api/Platforms/Template");
        return await this.getEntitiesInfo("/api/Platforms/default");
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
        //return await this.getEntitiesInfo("/api/ArmEdits/ShortViews");
        return await this.getEntitiesInfo("/api/ArmEdits");
    }
    async getTableArmEdits(){
        //return await this.getEntitiesInfo("/api/ArmEdits/TableViews");
        return await this.getEntitiesInfo("/api/ArmEdits");
    }
    async getArmEditTemplate(){
        //return await this.getEntitiesInfo("/api/ArmEdits/Template");
        return await this.getEntitiesInfo("/api/ArmEdits/default");
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
        //return await this.getEntitiesInfo("/api/Authors/ShortViews");
        return await this.getEntitiesInfo("/api/Authors");
    }
    async getTableAuthors(){
        //return await this.getEntitiesInfo("/api/Authors/TableViews");
        return await this.getEntitiesInfo("/api/Authors");
    }
    async getAuthorTemplate(){
        //return await this.getEntitiesInfo("/api/Authors/Template");
        return await this.getEntitiesInfo("/api/Authors/default");
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
        //return await this.getEntitiesInfo("/api/Communications/ShortViews");
        return await this.getEntitiesInfo("/api/Communications");
    }
    async getTableCommunications(){
        //return await this.getEntitiesInfo("/api/Communications/TableViews");
        return await this.getEntitiesInfo("/api/Communications");
    }
    async getCommunicationTemplate(){
        //return await this.getEntitiesInfo("/api/Communications/Template");
        return await this.getEntitiesInfo("/api/Communications/default");
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
        //return await this.getEntitiesInfo("/api/RelayAlgorithms/ShortViews");
        return await this.getEntitiesInfo("/api/RelayAlgorithms");
    }
    async getTableRelayAlgorithms(){
        //return await this.getEntitiesInfo("/api/RelayAlgorithms/TableViews");
        return await this.getEntitiesInfo("/api/RelayAlgorithms");
    }
    async getRelayAlgorithmTemplate(){
        //return await this.getEntitiesInfo("/api/RelayAlgorithms/Template");
        return await this.getEntitiesInfo("/api/RelayAlgorithms/default");
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
        //return await this.getEntitiesInfo("/api/ProjectVersions/ShortViews");
        return await this.getEntitiesInfo("/api/ProjectVersions");
    }
    async getTableProjectVersions(){
        //return await this.getEntitiesInfo("/api/ProjectVersions/TableViews");
        return await this.getEntitiesInfo("/api/ProjectVersions");
    }
    async getProjectVersionTemplate(){
        //return await this.getEntitiesInfo("/api/ProjectVersions/Template");
        return await this.getEntitiesInfo("/api/ProjectVersions/default");
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
        //return await this.getEntitiesInfo("/api/ProjectRevisions/TableViews");
        return await this.getEntitiesInfo("/api/ProjectRevisions");
    }
    async getTemplateProjectRevison(projectVersion = {}){
        //return await this.getEntityDetails("/api/ProjectRevisions/Template", projectVersion);
        return await this.getEntityDetails("/api/ProjectRevisions/ByProjectVersionId", projectVersion);
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