class RelayAlgorithm{
    constructor(){
        this.url = entitiesRepository.getRelayAlgorithmsUrl();
        this.editable = null;
        this.editFunc = null;
    }

    // получить автора по умолчанию:
    async defaultInitialize(){
        this.editable = await entitiesRepository.getDefaultEntity(this.url);

        // отправить данные:
        this.submit = async function(){
            let answer = await entitiesRepository.createEntity(this.url, this.editable);
            if(typeof(beforeEnding) === "function"){
                await beforeEnding(this.url, answer);
            }
        };
    }

    // получить конкретного автора из bd:
    async initialize(entityInfo){
        this.editable = await entitiesRepository.getEntityDetails(this.url, entityInfo);

        // отправить данные:
        this.submit = async function(){
            let answer = await entitiesRepository.updateEntity(this.url, this.editable);
            if(typeof(beforeEnding) === "function"){
                await beforeEnding(this.url, answer);
            }
        };
    }

    //
    async beforeEnding(url, answer){

    }

    getTitle(){
        return this.editable.title;
    }

    setTitle(newTitle = ""){
        this.editable.title = newTitle;
    }

    getANSI(){
        return this.editable.ansi;
    }

    setANSI(newansi = ""){
        this.editable.ansi = newansi;
    }

    getLogicalNode(){
        return this.editable.logicalnode;
    }

    setLogicalNode(newLN = ""){
        this.editable.logicalnode = newLN;
    }

    getDescription(){
        return this.editable.description;
    }

    setDescription(newDescription = ""){
        this.editable.description = newDescription;
    }
}