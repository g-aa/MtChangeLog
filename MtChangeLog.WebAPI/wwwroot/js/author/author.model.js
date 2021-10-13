class Author{
    constructor(){
        this.url = entitiesRepository.getAuthorsUrl();
        this.editable = null;
        this.editFunc = null;
    }

    // получить автора по умолчанию:
    async defaultInitialize(){
        this.editable = await entitiesRepository.getDefaultEntity(this.url);

        // отправить данные:
        this.submit = async function(){
            let answer = await entitiesRepository.createEntity(this.url, this.editable);
            if(typeof(this.beforeEnding) === "function"){
                await this.beforeEnding(this.url, answer);
            }
        };
    }

    // получить конкретного автора из bd:
    async initialize(entityInfo){
        this.editable = await entitiesRepository.getEntityDetails(this.url, entityInfo);

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

    getFirstName(){
        return this.editable.firstName;
    }

    setFirstName(newName = ""){
        this.editable.firstName = newName;
    }

    getLastName(){
        return this.editable.lastName;
    }

    setLastName(newName = ""){
        this.editable.lastName = newName;
    }

    getPosition(){
        return this.editable.position;
    }

    setPosition(newPosition = ""){
        this.editable.position = newPosition;
    }
}