class ArmEdit{
    constructor(){
        this.url = entitiesRepository.getArmEditsUrl();
        this.editable = null;
        this.editFunc = null;
    }

    // получить armEdit по умолчанию:
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

    // получить конкретный armEdit из bd:
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

    getDivg(){
        return this.editable.divg;
    }

    setDivg(newDivg = ""){
        this.editable.divg = newDivg;
    }

    getVersion(){
        return this.editable.version;
    }

    setVersion(newVersion = ""){
        this.editable.version = newVersion;
    }

    getDate(){
        return this.editable.date;
    }

    setDate(newDate = {}){
        this.editable.date = newDate;
    }

    getDescription(){
        return this.editable.description;
    }

    setDescription(newDesc = ""){
        this.editable.description = newDesc;
    }
}