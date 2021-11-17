class RelayAlgorithm{
    constructor(){
        this.editable = {};
    }

    // получить автора по умолчанию:
    async defaultInitialize(){
        // получить шаблон:
        this.editable = await repository.getRelayAlgorithmTemplate();

        // отправить данные:
        this.submit = async function(){
            let answer = await repository.createRelayAlgorithm(this.editable);
            if(typeof(this.beforeEnding) === "function"){
                await this.beforeEnding(answer);
            }
        };
    }

    // получить конкретного автора из bd:
    async initialize(entityInfo){
        // получить из БД:
        this.editable = await repository.getRelayAlgorithmDetails(entityInfo);

        // отправить данные:
        this.submit = async function(){
            let answer = await repository.updateRelayAlgorithm(this.editable);
            if(typeof(this.beforeEnding) === "function"){
                await this.beforeEnding(answer);
            }
        };
    }

    //
    async beforeEnding(answer){

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
        return this.editable.logicalNode;
    }

    setLogicalNode(newLN = ""){
        this.editable.logicalNode = newLN;
    }

    getDescription(){
        return this.editable.description;
    }

    setDescription(newDescription = ""){
        this.editable.description = newDescription;
    }
}