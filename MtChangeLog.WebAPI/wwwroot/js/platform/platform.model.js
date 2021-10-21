class Platform{
    constructor(){
        this.url = entitiesRepository.getPlatformsUrl();
        this.editable = { };
        this.modules = [ ];
        this.editFunc = null;
    }

    // получить platform по умолчанию:
    async defaultInitialize(){
        this.editable = await entitiesRepository.getDefaultEntity(this.url);

        let urlModule = entitiesRepository.getAnalogModulesUrl();
        this.modules = await entitiesRepository.getEntitiesInfo(urlModule);

        // отправить данные:
        this.submit = async function(){
            let answer = await entitiesRepository.createEntity(this.url, this.editable);
            if(typeof(this.beforeEnding) === "function"){
                await this.beforeEnding(this.url, answer);
            }
        };
    } 

    // получить конкретный platform из bd:
    async initialize(entityInfo){
        this.editable = await entitiesRepository.getEntityDetails(this.url, entityInfo);

        let urlModule = entitiesRepository.getAnalogModulesUrl();
        this.modules = await entitiesRepository.getEntitiesInfo(urlModule);

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