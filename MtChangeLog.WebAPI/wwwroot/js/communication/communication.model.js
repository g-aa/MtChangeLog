class Communication{
    constructor(){
        this.editable = {};
    }

    // получить автора по умолчанию:
    async defaultInitialize(){
        // получить шаблон:
        this.editable = await repository.getCommunicationTemplate();

        // отправить данные:
        this.submit = async function(){
            let answer = await repository.createCommunication(this.editable);
            if(typeof(this.beforeEnding) === "function"){
                await this.beforeEnding(answer);
            }
        };
    }

    // получить конкретного автора из bd:
    async initialize(entityInfo){
        this.editable = await repository.getCommunicationDetails(entityInfo);

        // отправить данные:
        this.submit = async function(){
            let answer = await repository.updateCommunication(this.editable);
            if(typeof(this.beforeEnding) === "function"){
                await this.beforeEnding(answer);
            }
        };
    }

    //
    async beforeEnding(answer){

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