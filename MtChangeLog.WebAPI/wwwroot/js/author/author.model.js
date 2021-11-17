class Author{
    constructor(){
        this.editable = {};
    }

    // получить автора по умолчанию:
    async defaultInitialize(){
        // получить шаблон:
        this.editable = await repository.getAuthorTemplate();

        // отправить данные:
        this.submit = async function(){
            let answer = await repository.createAuthor(this.editable);
            if(typeof(this.beforeEnding) === "function"){
                await this.beforeEnding(answer);
            }
        };
    }

    // получить конкретного автора из bd:
    async initialize(entityInfo){
        // получить из БД:
        this.editable = await repository.getAuthorDetails(entityInfo);

        // отправить данные:
        this.submit = async function(){
            let answer = await repository.updateAuthor(this.editable);
            if(typeof(this.beforeEnding) === "function"){
                await this.beforeEnding(answer);
            }
        };
    }

    //
    async beforeEnding(answer){

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