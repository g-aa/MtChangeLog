class EntitiesRepository {
    constructor() {
        this.urlAnalogModules = "/api/AnalogModules";
        this.urlPlatforms = "/api/Platforms";
        this.urlProjectsVersions = "/api/ProjectsVersions";
        this.urlProjectsRevisions = "/api/ProjectsRevisions";
    }

    getAnalogModulesUrl() {
        return this.urlAnalogModules;
    }

    getPlatformsUrl() {
        return this.urlPlatforms;
    }

    getProjectsVersionsUrl() {
        return this.urlProjectsVersions;
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