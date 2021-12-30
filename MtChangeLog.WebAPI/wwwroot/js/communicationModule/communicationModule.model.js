class CommunicationModule{
    constructor(){
        this.editable = {};
        this.protocols = [];
        this.configure = async function(){
            this.protocols = await repository.getShortProtocols();
        }
    }

    // получить автора по умолчанию:
    async defaultInitialize(){
        // получить шаблон:
        this.editable = await repository.getCommunicationModuleTemplate();
        await this.configure();

        // отправить данные:
        this.submit = async function(){
            let answer = await repository.createCommunicationModule(this.editable);
            if(typeof(this.beforeEnding) === "function"){
                await this.beforeEnding(answer);
            }
        };
    }

    // получить конкретного автора из bd:
    async initialize(entityInfo){
        this.editable = await repository.getCommunicationModuleDetails(entityInfo);
        await this.configure();

        // отправить данные:
        this.submit = async function(){
            let answer = await repository.updateCommunicationModule(this.editable);
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

    setDescription(newDescription = ""){
        this.editable.description = newDescription;
    }

    getAllProtocols(){
        return this.protocols;
    }

    getProtocols(){
        return this.editable.protocols;
    }

    setProtocols(ids = [""]){
        if (ids === undefined){
            throw new Error("не указан перечень protocols ids!");
        }
        this.editable.protocols = this.protocols.filter(function(item){
            return ids.indexOf(item.id) != -1;
        });
    }
}