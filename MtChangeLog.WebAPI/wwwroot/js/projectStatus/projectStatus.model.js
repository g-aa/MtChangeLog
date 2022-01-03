class ProjectStatus{
    constructor(){
        this.editable = {};
    }

    // получить статус проекта по умолчанию:
    async defaultInitialize(){
        // получить шаблон:
        this.editable = await repository.getProjectStatusTemplate();
        
        // отправить данные:
        this.submit = async function(){
            let answer = await repository.createProjectStatus(this.editable);
            if(typeof(this.beforeEnding) === "function"){
                await this.beforeEnding(answer);
            }
        };
    }

    // получить конкретный статус проекта из db:
    async initialize(entityInfo){
        // получить из БД:
        this.editable = await repository.getProjectStatusDetails(entityInfo);
        
        // отправить данные:
        this.submit = async function(){
            let answer = await repository.updateProjectStatus(this.editable);
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
}