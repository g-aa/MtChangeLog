class ProtocolTableLayout{
    constructor(parentLayout){
        this.parentLayout = parentLayout;

        let tableLayputId = "protocolTable_id";
        this.tableLayout = {
            view:"datatable",
            id:tableLayputId, 
            select:"row",
            adjust:true,
            resizeColumn:true,
            scroll:"xy",
            columns:[
                //{ id:"id",          adjust:true, header:["GUID:", ""] },
                { id:"title",       width:150, header:["Наименование:", { content:"multiSelectFilter" }] },
                { id:"description", adjust:true,    header:["Описание:", ""] },
                { fillspace:true }
            ],
            data:[]
        }

        // обновление таблицы:
        let refresh = async function(){
            let tableData = await repository.getTableProtocols();
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
                            let entity = new Protocol();
                            await entity.defaultInitialize();
                            entity.beforeEnding = async function(answer){
                                await refresh();
                                messageBox.information(answer);
                            }
                            let win = new ProtocolEditWindow(entity);
                            win.show();
                        } catch (error) {
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
                            if(selected != undefined) {
                                let entity = new Protocol();
                                await entity.initialize(selected);
                                entity.beforeEnding = async function(answer){
                                    await refresh();
                                    messageBox.information(answer);
                                }
                                let win = new ProtocolEditWindow(entity);
                                win.show();
                            } else{
                                messageBox.information("выберете протокол инф. обмена для редактирования");
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
                                let answer = await repository.deleteProjectStatus(selected);
                    
                                // удалить обьект в UI части:
                                $$(tableLayputId).remove(selected.id);
                                messageBox.information(answer);
                            } else{
                                messageBox.information("выберете протокол инф. обмена для удаления");
                            }
                        } catch (error){
                            messageBox.error(error.message); 
                        }
                    } 
                }
            ]
        }
    }

    show(){
        webix.ui({
            view: "layout",
            rows: [ this.buttonsLayout, this.tableLayout ]
        }, 
        this.parentLayout.getChildViews()[0]);
        this.refresh().catch(error => {
            messageBox.error(error.message);
        });
    }
}