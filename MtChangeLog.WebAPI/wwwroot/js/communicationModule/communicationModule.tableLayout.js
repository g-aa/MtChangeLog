class CommunicationModuleTableLayout{
    constructor(){
        let tableLayputId = "communicationTable_id";
        this.tableLayout = {
            view:"datatable",
            id:tableLayputId, 
            select:"row",
            adjust:true,
            resizeColumn:true,
            scroll:"xy",
            columns:[
                //{ id:"id",          adjust:true,    header:["GUID:", ""] },
                { id:"title",       width:150,      header:["Наименование:", { content:"multiSelectFilter" }] },
                { id:"protocols",   width:850,       header:["Перечень протоколов:", ""] },
                { id:"description", adjust:true,    header:["Описание:", ""] },
                { fillspace:true }
            ],
            data:[]
        }

        // обновление таблицы:
        let refresh = async function(){
            let tableData = await repository.getTableCommunicationModules();
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
                            mainLayout.showProgress();
                            let communication = new CommunicationModule();
                            await communication.defaultInitialize();
                            communication.beforeEnding = async function(answer){
                                await refresh();
                                messageBox.information(answer);
                            };
                            let win = new CommunicationModuleEditWindow(communication);
                            win.show();
                        } catch (error){
                            messageBox.error(error.message);
                        } finally{
                            mainLayout.closeProgress();
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
                            mainLayout.showProgress();
                            let selected = $$(tableLayputId).getSelectedItem();
                            if(selected != undefined){
                                let communication = new CommunicationModule();
                                await communication.initialize(selected);
                                communication.beforeEnding = async function(answer){
                                    await refresh();
                                    messageBox.information(answer);
                                };
                                let win = new CommunicationModuleEditWindow(communication);
                                win.show();
                            } else{
                                messageBox.information("выберете перечень протоколов для редактирования");
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
                            let selected = $$(tableLayputId).getSelectedItem();
                            if(selected != undefined){
                                // удалить обьект на сервере:
                                let answer = await repository.deleteCommunicationModule(selected);
                    
                                // удалить обьект в UI части:
                                $$(tableLayputId).remove(selected.id);
                                messageBox.information(answer);
                            } else{
                                messageBox.information("выберете перечень протоколов для удаления");
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
            view:"layout",
            rows:[ this.buttonsLayout, this.tableLayout ]
        }, 
        parentLayout.getChildViews()[0]);
        await this.refresh();
    }
}