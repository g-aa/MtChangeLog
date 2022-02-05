class ProjectRevisionLayout{
    constructor(){
        let tableLayoutId = "projectRevisionTable_id";
        this.tableLayout = {
            view:"datatable",
            id:tableLayoutId,
            select:"row",
            adjust:true,
            resizeColumn:true,
            scroll:"xy",
            columns:[
                //{ id:"id",         adjust:true,       header:["GUID:", ""] },
                { id:"module",     width:150,   minWidth:150,   header:["Аналоговый модуль:", { content:"multiSelectFilter" }] },
                { id:"title",      adjust:true, header:["Наименование:", { content:"multiSelectFilter" }] },
                { id:"version",    width:100,   header:["Версия:", { content:"multiSelectFilter" }] },
                { id:"revision",   width:100,   header:["Ревизия:", { content:"multiSelectFilter" }] },
                { id:"date",       adjust:true, template:function(obj){ 
                        let format = webix.Date.dateToStr("%Y-%m-%d");
                        return format(new Date(obj.date)); 
                    },header:["Дата:", ""] },
                { id:"armEdit",    width:170,   header:["ArmEdit:", { content:"multiSelectFilter" }] },
                { id:"reason",     adjust:true, header:["Причина:", ""] },
                { fillspace:true }
            ],
            data:[]
        }

        // обновление таблицы:
        let refresh = async function(){
            let tableData = await repository.getTableProjectRevisions();
            let tableLayout = $$(tableLayoutId);
            tableLayout.parse(tableData);
            tableLayout.refresh();
        }
        this.refresh = refresh;

        this.buttonsLayout = {
            view:"toolbar", 
            cols:[
                { 
                    view:"button", 
                    css:"webix_secondary",
                    value:"Редактировать", 
                    width:200,
                    click: async function (){
                        try{
                            mainLayout.showProgress();
                            let selected = $$(tableLayoutId).getSelectedItem();
                            if(selected != undefined){
                                let revision = new ProjectRevision();
                                await revision.initialize(selected);
                                revision.beforeEnding = async function(answer){
                                    await refresh();
                                    messageBox.information(answer);
                                };
                                let win = new ProjectRevisionEditWindow(revision);
                                win.show();
                            } else{
                                messageBox.information("выберете редакцию проекта для работы");
                            }
                        } catch (error){
                            messageBox.error(error.message);
                        } finally{
                            mainLayout.closeProgress();
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
                            mainLayout.showProgress();
                            let selected = $$(tableLayoutId).getSelectedItem();
                            if(selected != undefined){
                                // удалить обьект на сервере:
                                let answer = await repository.deleteProjectRevison(selected);
                              
                                // удалить обьект в UI части:
                                $$(tableLayoutId).remove(selected.id);
                                messageBox.information(answer);
                            } else {
                                messageBox.information("выберете ревизию проекта для удаления");
                            }
                        } catch (error){
                            messageBox.error(error.message); 
                        } finally{
                            mainLayout.closeProgress();
                        }
                    } 
                }
            ]
        }
    }

    async show(parentLayout){
        webix.ui({
            view: "layout",
            rows: [ this.buttonsLayout, this.tableLayout ]
        }, 
        parentLayout.getChildViews()[0]);
        await this.refresh();
    }
}