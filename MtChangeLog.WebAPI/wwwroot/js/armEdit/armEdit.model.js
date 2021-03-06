class ArmEdit{
    constructor(){
        this.editable = {};
    }

    // получить armEdit по умолчанию:
    async defaultInitialize(){
        // получить шаблон:
        this.editable = await repository.getArmEditTemplate();

        // отправить данные:
        this.submit = async function(){
            let answer = await repository.createArmEdit(this.editable);
            if(typeof(this.beforeEnding) === "function"){
                await this.beforeEnding(answer);
            }
        };
    }

    // получить конкретный armEdit из bd:
    async initialize(entityInfo){
        // получить из БД:
        this.editable = await repository.getArmEditDetails(entityInfo);

        // отправить данные:
        this.submit = async function(){
            let answer = await repository.updateArmEdit(this.editable);
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
        return new Date(this.editable.date);
    }

    setDate(newDate = ""){
        this.editable.date =  newDate;
    }

    getDescription(){
        return this.editable.description;
    }

    setDescription(newDesc = ""){
        this.editable.description = newDesc;
    }
}