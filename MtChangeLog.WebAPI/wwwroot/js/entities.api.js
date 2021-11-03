class EntitiesRepository {
    constructor() {
        this.urlAnalogModules =             "/api/AnalogModules";
        this.urlArmEdits =                  "/api/ArmEdits";
        this.urlAuthors =                   "/api/Authors";
        this.urlCommunications =            "/api/Communications";
        this.urlPlatforms =                 "/api/Platforms";
        this.urlProjectsVersions =          "/api/ProjectVersions";
        this.urlProjectsRevisions =         "/api/ProjectRevisions";
        this.urlParentProjectsRevisions =   "/api/ProjectRevisions/ShortViews",
        this.urlProjectsRevisionsTemplate = "/api/ProjectRevisions/ByProjectVersionId",
        this.urlRelayAlgorithms =           "/api/RelayAlgorithms";
    }

    getAnalogModulesUrl() {
        return this.urlAnalogModules;
    }

    getArmEditsUrl() {
        return this.urlArmEdits;
    }

    getAuthorsUrl() {
        return this.urlAuthors;
    }

    getCommunicationsUrl() {
        return this.urlCommunications;
    }

    getPlatformsUrl() {
        return this.urlPlatforms;
    }

    getProjectsVersionsUrl() {
        return this.urlProjectsVersions;
    }

    getProjectsRevisionsUrl() {
        return this.urlProjectsRevisions;
    }

    getParentProjectsRevisionsUrl() {
        return this.urlParentProjectsRevisions;
    }

    getProjectsRevisionsTemplateUrl() {
        return this.urlProjectsRevisionsTemplate;
    }

    getRelayAlgorithmsUrl() {
        return this.urlRelayAlgorithms;
    }

    async getEntitiesInfo(urlString) {
        let response = await fetch(urlString, {
            method: "GET",
            headers: { 
                "Accept": "application/json" 
            }
        });
        let answer = await response.json();
        if (response.ok) {
            return answer;
        }
        else {
            throw new Error("HTTP GET error<br>" + response.status + " - " + response.statusText + "<br>" + answer);
        }
    }
    
    async getEntityDetails(urlString, module = { }) {
        let response = await fetch(urlString + "/" + module.id, {
            method: "GET",
            headers: { 
                "Accept": "application/json" 
            }
        });
        let answer = await response.json();
        if (response.ok) {
            return answer;
        }
        else {
            throw new Error("HTTP GET error<br>" + response.status + " - " + response.statusText + "<br>" + answer);
        }
    }

    async createEntity(urlString, module = { }) {
        let response = await fetch(urlString, {
            method: "POST",
            headers: { 
                "Accept": "application/json", 
                "Content-Type": "application/json" 
            },
            body: JSON.stringify(module)
        });
        let answer = await response.json();
        if (response.ok) {
            return answer;
        }
        else {
            throw new Error("HTTP POST error<br>" + response.status + " - " + response.statusText + "<br>" + answer);
        }
    }
    
    async updateEntity(urlString, module = { }) {
        let response = await fetch(urlString + "/" + module.id, {
            method: "PUT",
            headers: {
                "Accept": "application/json", 
                "Content-Type": "application/json" 
            },
            body: JSON.stringify(module)
        });
        let answer = await response.json();
        if (response.ok) {
            return answer;
        }
        else {
            throw new Error("HTTP PUT error<br>" + response.status + " - " + response.statusText + "<br>" + answer);
        }
    }
    
    async deleteEntity(urlString, module = { }) {
        let response = await fetch(urlString + "/" + module.id, {
            method: "DELETE",
            headers: { 
                "Accept": "application/json" 
            }
        });
        let answer = await response.json();
        if (response.ok) {
            return answer;
        }
        else {
            throw new Error("HTTP DELETE error<br>" + response.status + " - " + response.statusText + "<br>" + answer);
        }
    }

    async getDefaultEntity(urlString) {
        return await this.getEntitiesInfo(urlString + "/default");
    }

    async getProjectStatuses() {
        return await this.getEntitiesInfo(this.urlProjectsVersions + "/statuses");
    }

}

if(entitiesRepository == undefined || entitiesRepository == null) {
    var entitiesRepository = new EntitiesRepository();
}