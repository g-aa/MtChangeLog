class Platform{
    constructor(){
        this.editable = {};
        this.modules = [];
        this.configure = async function(){
            this.modules = await repository.getSortAnalogModules();
        }
    }

    // получить platform по умолчанию:
    async defaultInitialize(){
        // получить шаблон:
        this.editable = await repository.getPlatformTemplate();
        await this.configure();

        // отправить данные:
        this.submit = async function(){
            let answer = await repository.createPlatform(this.editable);
            if(typeof(this.beforeEnding) === "function"){
                await this.beforeEnding(answer);
            }
        };
    }

    // получить конкретный platform из bd:
    async initialize(entityInfo){
        // получить из БД:
        this.editable = await repository.getPlatformDetails(entityInfo);
        await this.configure();

        // отправить данные:
        this.submit = async function(){
            let answer = await repository.updatePlatform(this.editable);
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

    setTitle(value = ""){
        this.editable.title = value;
    }

    getDescription(){
        return this.editable.description;
    }

    setDescription(value = ""){
        this.editable.description = value;
    }

    getAllAnalogModules(){
        return this.modules;
    }

    getAnalogModules(){
        return this.editable.analogModules;
    }

    setAnalogModules(values = []){
        this.editable.analogModules = values;
    }
}