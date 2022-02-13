class EntitiesRepository{
    constructor(){
        let parsing = async function(method, response){
            let answer = await response.json();
            if (response.ok) {
                return answer;
            } else{
                let message = "HTTP " + method + "<br>" + response.status + "-" + response.statusText + "<br>";
                if(typeof answer === "string"){
                    message += answer;
                } else if(answer.hasOwnProperty("errors")){
                    Object.keys(answer.errors).forEach(function(key){
                        message += answer.errors[key].join("<br>") + "<br>";
                    });
                }
                throw new Error(message);
            }
        }

        // CRUD API:
        this.getEntitiesInfo = async function(urlString){
            let response = await fetch(urlString, {
                method:"GET",
                headers:{ 
                    "Accept":"application/json" 
                }
            });
            return await parsing("GET", response);
        }
        this.getEntityDetails = async function(urlString, module = {}){
            let response = await fetch(urlString + "/" + module.id, {
                method:"GET",
                headers:{ 
                    "Accept":"application/json" 
                }
            });
            return await parsing("GET", response);
        }
        this.createEntity = async function(urlString, module = {}){
            let response = await fetch(urlString, {
                method:"POST",
                headers:{ 
                    "Accept":"application/json", 
                    "Content-Type":"application/json" 
                },
                body:JSON.stringify(module)
            });
            return await parsing("POST", response);
        }
        this.updateEntity = async function(urlString, module = {}){
            let response = await fetch(urlString + "/" + module.id, {
                method:"PUT",
                headers:{
                    "Accept":"application/json", 
                    "Content-Type":"application/json" 
                },
                body:JSON.stringify(module)
            });
            return await parsing("PUT", response);
        }
        this.deleteEntity = async function(urlString, module = {}){
            let response = await fetch(urlString + "/" + module.id, {
                method:"DELETE",
                headers:{ 
                    "Accept":"application/json" 
                }
            });
            return await parsing("DELETE", response);
        }
    }

    // Аналоговые модули:
    async getShortAnalogModules(){
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
    async getShortPlatforms(){
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
    async getShortArmEdits(){
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
    async getShortAuthors(){
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

    // протоколы инф. обмена:
    async getShortProtocols(){
        return await this.getEntitiesInfo("/api/Protocols/ShortViews");
    }
    async getTableProtocols(){
        return await this.getEntitiesInfo("/api/Protocols/TableViews");
    }
    async getProtocolTemplate(){
        return await this.getEntitiesInfo("/api/Protocols/Template");
    }
    async getProtocolDetails(entityInfo = {}){
        return await this.getEntityDetails("/api/Protocols", entityInfo);
    }
    async createProtocol(entity = {}){
        return await this.createEntity("/api/Protocols", entity);
    }
    async updateProtocol(entity = {}){
        return await this.updateEntity("/api/Protocols", entity);
    }
    async deleteProtocol(entity = {}){
        return await this.deleteEntity("/api/Protocols", entity);
    }

    // коммуникации:
    async getShortCommunicationModules(){
        return await this.getEntitiesInfo("/api/CommunicationModules/ShortViews");
    }
    async getTableCommunicationModules(){
        return await this.getEntitiesInfo("/api/CommunicationModules/TableViews");
    }
    async getCommunicationModuleTemplate(){
        return await this.getEntitiesInfo("/api/CommunicationModules/Template");
    }
    async getCommunicationModuleDetails(entityInfo = {}){
        return await this.getEntityDetails("/api/CommunicationModules", entityInfo);
    }
    async createCommunicationModule(entity = {}){
        return await this.createEntity("/api/CommunicationModules", entity);
    }
    async updateCommunicationModule(entity = {}){
        return await this.updateEntity("/api/CommunicationModules", entity);
    }
    async deleteCommunicationModule(entity = {}){
        return await this.deleteEntity("/api/CommunicationModules", entity);
    }

    // Релейные алгоритмы:
    async getShortRelayAlgorithms(){
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

    // Статус проекта:
    async getShortProjectStatuses(){
        return await this.getEntitiesInfo("/api/ProjectStatuses/ShortViews");
    }
    async getTableProjectStatuses(){
        return await this.getEntitiesInfo("/api/ProjectStatuses/TableViews");
    }
    async getProjectStatusTemplate(){
        return await this.getEntitiesInfo("/api/ProjectStatuses/Template");
    }
    async getProjectStatusDetails(entityInfo = {}){
        return await this.getEntityDetails("/api/ProjectStatuses", entityInfo);
    }
    async createProjectStatus(entity = {}){
        return await this.createEntity("/api/ProjectStatuses", entity);
    }
    async updateProjectStatus(entity = {}){
        return await this.updateEntity("/api/ProjectStatuses", entity);
    }
    async deleteProjectStatus(entity = {}){
        return await this.deleteEntity("/api/ProjectStatuses", entity);
    }

    // Версии проектов:
    async getShortProjectVersions(){
        return await this.getEntitiesInfo("/api/ProjectVersions/ShortViews");
    }
    async getTableProjectVersions(){
        return await this.getEntitiesInfo("/api/ProjectVersions/TableViews");
    }
    async getProjectVersionTemplate(){
        return await this.getEntitiesInfo("/api/ProjectVersions/Template");
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
    async getShortProjectRevisions(){
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
    async getProjectVersionHistory(entityId = ""){
        return await this.getEntityDetails("/api/ProjectHistory/Version", { id:entityId });
    }
    async getProjectRevisionHistory(entityId = ""){
        return await this.getEntityDetails("/api/ProjectHistory/Revision", { id:entityId });
    }
    
    // Статистика:
    async getShortStatistics(){
        return await this.getEntitiesInfo("/api/Statistics/Short");
    }
    async getLastProjectsRevision(){
        return await this.getEntitiesInfo("/api/Statistics/LastProjectsRevision");
    }
    async getAuthorProjectContributions(){
        return await this.getEntitiesInfo("/api/Statistics/AuthorProjectContributions");
    }

    // Файлы:
    async getProjectHistorysArchiveForExport(){
        return await this.getEntitiesInfo("/api/Files/ProjectHistorysArchive");
    }
    async getProjectHistoryForExport(entityId = ""){
        return await this.getEntityDetails("/api/Files/ProjectHistory", { id:entityId });
    }
}

var repository = new EntitiesRepository();