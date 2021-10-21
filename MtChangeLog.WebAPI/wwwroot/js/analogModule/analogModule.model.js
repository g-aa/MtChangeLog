class AnalogModule{
    constructor(){
        this.url = entitiesRepository.getAnalogModulesUrl();
        this.editable = { };
        this.platforms = [ ];
        this.editFunc = null;
    }

    // получить analog module по умолчанию:
    async defaultInitialize(){
        this.editable = await entitiesRepository.getDefaultEntity(this.url);

        let urlPlatform = entitiesRepository.getPlatformsUrl();
        this.platforms = await entitiesRepository.getEntitiesInfo(urlPlatform);

        // отправить данные:
        this.submit = async function(){
            let answer = await entitiesRepository.createEntity(this.url, this.editable);
            if(typeof(this.beforeEnding) === "function"){
                await this.beforeEnding(this.url, answer);
            }
        };
    } 

    // получить конкретный analog module из bd:
    async initialize(entityInfo){
        this.editable = await entitiesRepository.getEntityDetails(this.url, entityInfo);

        let urlPlatform = entitiesRepository.getPlatformsUrl();
        this.platforms = await entitiesRepository.getEntitiesInfo(urlPlatform);

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

    getDivg(){
        return this.editable.divg;
    }

    setDivg(value = ""){
        this.editable.divg = value;
    }

    getTitle(){
        return this.editable.title;
    }

    setTitle(value = ""){
        this.editable.title = value;
    }

    getNominalCurrent(){
        return this.editable.current;
    }

    setNominalCurrent(value = ""){
        this.editable.current = value;
    }

    getDescription(){
        return this.editable.description;
    }

    setDescription(value = ""){
        this.editable.description = value;
    }

    getAllPlatforms(){
        return this.platforms;
    }

    getPlatforms(){
        return this.editable.platforms;
    }

    setPlatforms(values = []){
        this.editable.platforms = values;
    }
}