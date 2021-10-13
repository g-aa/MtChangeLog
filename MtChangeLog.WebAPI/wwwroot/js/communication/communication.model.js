class Communication{
    constructor(){
        this.url = entitiesRepository.getCommunicationsUrl();
        this.editable = null;
        this.editFunc = null;
    }

    // получить автора по умолчанию:
    async defaultInitialize(){
        this.editable = await entitiesRepository.getDefaultEntity(this.url);

        // отправить данные:
        this.submit = async function(){
            let answer = await entitiesRepository.createEntity(this.url, this.editable);
            if(typeof(this.beforeEnding) === "function"){
                await this.beforeEnding(this.url, answer);
            }
        };
    }

    // получить конкретного автора из bd:
    async initialize(entityInfo){
        this.editable = await entitiesRepository.getEntityDetails(this.url, entityInfo);

        // отправить данные:
        this.submit = async function(){
            let answer = await entitiesRepository.updateEntity(this.url, this.editable);
            if(typeof(this.beforeEnding) === "function"){
                await this.beforeEnding(this.url, answer);
            }
        };
    }

    //
    async beforeEnding(url, answer){

    }

    getProtocols(){
        return this.editable.protocols;
    }

    setProtocols(newProtocols = ""){
        this.editable.protocols = newProtocols;
    }

    getDescription(){
        return this.editable.description;
    }

    setDescription(newDescription = ""){
        this.editable.description = newDescription;
    }
}