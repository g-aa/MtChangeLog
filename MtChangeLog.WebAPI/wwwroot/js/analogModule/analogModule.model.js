class AnalogModule{
    constructor(){
        this.editable = {};
        this.platforms = [];
        this.configure = async function(){
            this.platforms = await repository.getShortPlatforms();
        }
    }

    // получить analog module по умолчанию:
    async defaultInitialize(){
        // получить шаблон:
        this.editable = await repository.getAnalogModuleTemplate();
        await this.configure();

        // отправить данные:
        this.submit = async function(){
            let answer = await repository.createAnalogModule(this.editable);
            if(typeof(this.beforeEnding) === "function"){
                await this.beforeEnding(answer);
            }
        };
    } 

    // получить конкретный analog module из bd:
    async initialize(entityInfo){
        // получить из БД:
        this.editable = await repository.getAnalogModuleDetails(entityInfo);
        await this.configure();
        
        // отправить данные:
        this.submit = async function(){
            let answer = await repository.updateAnalogModule(this.editable);
            if(typeof(this.beforeEnding) === "function"){
                await this.beforeEnding(answer);
            }
        };
    }

    //
    async beforeEnding(answer){

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

    setPlatforms(ids = [""]){
        if (ids == undefined || ids == null){
            throw new Error("не указан перечень platforms id!");
        }
        this.editable.platforms = this.platforms.filter(function(item){
            return ids.indexOf(item.id) != -1;
        });
    }
}