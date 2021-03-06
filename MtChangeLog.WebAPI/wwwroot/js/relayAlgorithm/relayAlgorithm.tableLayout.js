class RelayAlgorithmTableLayout{
    constructor(){
        let tableLayputId = "relayAlgorithmTable_id";
        this.tableLayout = {
            view:"datatable",
            id:tableLayputId, 
            select:"row",
            adjust:true,
            resizeColumn:true,
            scroll:"xy",
            columns:[
                //{ id:"id",          adjust:true, header:["GUID:", ""] },
                { id:"group",       width:200,      header:["Наименование группы:", { content:"multiSelectFilter" }]},
                { id:"title",       width:200,      header:["Наименование:", { content:"multiSelectFilter" }] },
                { id:"ansi",        width:150,      header:["ANSI код:", { content:"multiSelectFilter" }] },
                { id:"logicalNode", width:150,      header:["LN:", { content:"multiSelectFilter" }] },
                { id:"description", adjust:true,    header:["Описание:", ""] },
                { fillspace:true }
            ],
            data:[]
        }

        // обновление таблицы:
        let refresh = async function(){
            let tableData = await repository.getTableRelayAlgorithms();
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
                            let algorithm = new RelayAlgorithm();
                            await algorithm.defaultInitialize();
                            algorithm.beforeEnding = async function(answer){
                                await refresh();
                                messageBox.information(answer);
                            };
                            let win = new RelayAlgorithmEditWindow(algorithm);
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
                                let algorithm = new RelayAlgorithm();
                                await algorithm.initialize(selected);
                                algorithm.beforeEnding = async function(answer){
                                    await refresh();
                                    messageBox.information(answer);
                                };
                                let win = new RelayAlgorithmEditWindow(algorithm);
                                win.show();
                            } else{
                                messageBox.information("выберете алгоритм РЗиА для редактирования");
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
                                let answer = await repository.deleteRelayAlgorithm(selected);
                    
                                // удалить обьект в UI части:
                                $$(tableLayputId).remove(selected.id);
                                messageBox.information(answer);
                            } else{
                                messageBox.information("выберете алгоритм для удаления");
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