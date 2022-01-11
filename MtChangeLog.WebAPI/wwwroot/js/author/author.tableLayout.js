class AuthorTableLayout{
    constructor(){
        let tableLayputId = "authorTable_id";
        this.tableLayout = {
            view:"datatable",
            id:tableLayputId, 
            select:"row",
            adjust:true,
            resizeColumn:true,
            scroll:"xy",
            columns:[
                //{ id:"id",          adjust:true,    header:["GUID:", ""] },
                { id:"lastName",    width:250,      header:["Фамилия:", { content:"multiSelectFilter" }] },
                { id:"firstName",   width:250,      header:["Имя:", { content:"multiSelectFilter" }] },
                { id:"position",    adjust:true,    header:["Должность:", { content:"multiSelectFilter" }] },
                { fillspace:true }
            ],
            data:[]
        }

        // обновление таблицы:
        let refresh = async function(){
            let tableData = await repository.getTableAuthors();
            let tableLaypu = $$(tableLayputId);
            tableLaypu.parse(tableData);
            tableLaypu.refresh();
        }
        this.refresh = refresh;

        this.buttonsLayout = {
            view:"toolbar", 
            cols:[
                { 
                    view:"button", 
                    css:"webix_primary",
                    value:"Добавить", 
                    width:200,
                    click: async function (){
                        try{
                            let author = new Author();
                            await author.defaultInitialize();
                            author.beforeEnding = async function(answer){
                                await refresh();
                                messageBox.information(answer);
                            };
                            let win = new AuthorEditWindow(author);
                            win.show();
                        } catch (error){
                            messageBox.error(error.message);
                        }
                    }
                },
                { 
                    view:"button", 
                    css:"webix_secondary",
                    value:"Редактировать", 
                    width:200,
                    click: async function (){
                        try{
                            let selected = $$(tableLayputId).getSelectedItem();
                            if(selected != undefined){
                                let author = new Author();
                                await author.initialize(selected);
                                author.beforeEnding = async function(answer){
                                    await refresh();
                                    messageBox.information(answer);
                                };
                                let win = new AuthorEditWindow(author);
                                win.show();
                            } else{
                                messageBox.information("выберете автора для редактирования");
                            }
                        } catch (error){
                            messageBox.error(error.message);
                        }
                    }
                },
                { 
                    view:"button",
                    css:"webix_danger",
                    value:"Удалить",
                    width:200,
                    align:"right",
                    click: async function (){
                        try{
                            let selected = $$(tableLayputId).getSelectedItem();
                            if(selected != undefined){
                                // удалить обьект на сервере:
                                let answer = await repository.deleteAuthor(selected);
                    
                                // удалить обьект в UI части:
                                $$(tableLayputId).remove(selected.id);
                                messageBox.information(answer);
                            } else{
                                messageBox.information("выберете аналоговый модуль для удаления");
                            }
                        } catch (error){
                            messageBox.error(error.message); 
                        }
                    } 
                }
            ]
        }
    }

    show(parentLayout){
        webix.ui({
            view:"layout",
            rows:[ this.buttonsLayout, this.tableLayout ]
        }, 
        parentLayout.getChildViews()[0]);
        this.refresh().catch(error => {
            messageBox.error(error.message);
        });
    }
}