class AnalogModuleTableLayout{
    constructor(parentLayout){
        this.parentLayout = parentLayout;
        
        let tableLayputId = "analogModuleTable_id";
        this.tableLayout = {
            view:"datatable",
            id:tableLayputId, 
            select:"row",
            adjust:true,
            resizeColumn:true,
            scroll:"xy",
            columns:[
                //{ id:"id",        adjust: true,    header: ["GUID:", ""] },
                { id:"title",       width:150,       header:["Наименование:", { content:"multiSelectFilter" }] },
                { id:"divg",        adjust:true,     header:["ДИВГ:", ""] },
                { id:"current",     adjust:"header", header:["Номинальный ток:", { content:"multiSelectFilter" }] },
                { id:"description", adjust:true,     header:["Описание:", ""] },
                { fillspace:true }
            ],
            data:[]
        }

        // обновление таблицы:
        let refresh = async function(){
            let tableData = await repository.getTableAnalogModules();
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
                            let module = new AnalogModule();
                            await module.defaultInitialize();
                            module.beforeEnding = async function(answer){
                                await refresh();
                                messageBox.information(answer);
                            };
                            let win = new AnalogModuleEditWindow(module);
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
                                let module = new AnalogModule();
                                await module.initialize(selected);
                                module.beforeEnding = async function(answer){
                                    await refresh();
                                    messageBox.information(answer);
                                };
                                let win = new AnalogModuleEditWindow(module);
                                win.show();
                            } else{
                                messageBox.information("выберете аналоговый модуль для редактирования");
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
                                let answer = await repository.deleteAnalogModule(selected);
                    
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

    show(){
        webix.ui({
            view:"layout",
            rows:[ this.buttonsLayout, this.tableLayout ]
        }, 
        this.parentLayout.getChildViews()[0]);
        this.refresh().catch(error => {
            messageBox.error(error.message);
        });  
    }
}