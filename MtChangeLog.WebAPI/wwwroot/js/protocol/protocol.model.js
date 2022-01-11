class Protocol{
    constructor(){
        this.editable = {};
        this.communicationModules = [];
        this.configure = async function(){
            this.communicationModules = await repository.getShortCommunicationModules();
        }
    }

    // получить статус проекта по умолчанию:
    async defaultInitialize(){
        // получить шаблон:
        this.editable = await repository.getProtocolTemplate();
        await this.configure();

        // отправить данные:
        this.submit = async function(){
            let answer = await repository.createProtocol(this.editable);
            if(typeof(this.beforeEnding) === "function"){
                await this.beforeEnding(answer);
            }
        };
    }

    // получить конкретный статус проекта из db:
    async initialize(entityInfo){
        // получить из БД:
        this.editable = await repository.getProtocolDetails(entityInfo);
        await this.configure();

        // отправить данные:
        this.submit = async function(){
            let answer = await repository.updateProtocol(this.editable);
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

    getDescription(){
        return this.editable.description;
    }

    setDescription(newDesc = ""){
        this.editable.description = newDesc;
    }

    getAllCommunicationModules(){
        return this.communicationModules;
    }

    getCommunicationModules(){
        return this.editable.communicationModules;
    }

    setCommunicationModules(ids = [""]){
        if (ids === undefined){
            throw new Error("не указан перечень communication modules ids!");
        }
        this.editable.communicationModules = this.communicationModules.filter(function(item){
            return ids.indexOf(item.id) != -1;
        });
    }
}