class RelayAlgorithmTableLayout{
    constructor(parentLayout){
        this.parentLayout = parentLayout;
        this.tableLayout = {
            view:"datatable",
            id:"relayAlgorithmTable_id", 
            select:"row",
            adjust:true,
            resizeColumn:true,
            columns:[
                { id:"id",          adjust:true, header:["GUID:", ""] },
                { id:"title",       adjust:true, header:["Наименование:", { content:"multiSelectFilter" }] },
                { id:"ansi",        adjust:true, header:["ANSI код:", { content:"multiSelectFilter" }] },
                { id:"logicalNode", adjust:true, header:["LN:", { content:"multiSelectFilter" }] },
                { id:"description", fillspace:true, header:["Описание:", ""] }
            ],
            data:[]
        }

        let btnWidth = 200;
        this.buttonsLayout = {
            view:"toolbar", 
            cols:[
                { 
                    view:"button", 
                    css:"webix_primary",
                    value:"Добавить", 
                    width:btnWidth,
                    click: async function (){
                        try{
                            let algorithm = new RelayAlgorithm();
                            await algorithm.defaultInitialize();
                            algorithm.beforeEnding = async function(url, answer){
                                messageBox.information(answer);
                                
                                let tableData = await entitiesRepository.getEntitiesInfo(url);
                                let table = $$("relayAlgorithmTable_id");
                                table.parse(tableData);
                                table.refresh();
                            };

                            let win = new RelayAlgorithmEditWindow(algorithm);
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
                    width:btnWidth,
                    click: async function (){
                        try{
                            let selected = $$("relayAlgorithmTable_id").getSelectedItem();
                            if(selected != undefined){
                                let algorithm = new RelayAlgorithm();
                                await algorithm.initialize(selected);
                                algorithm.beforeEnding = async function(url, answer){
                                    messageBox.information(answer);
                                
                                    let tableData = await entitiesRepository.getEntitiesInfo(url);
                                    let table = $$("relayAlgorithmTable_id");
                                    table.parse(tableData);
                                    table.refresh();
                                };

                                let win = new RelayAlgorithmEditWindow(algorithm);
                                win.show();
                            } else{
                                messageBox.information("выберете алгоритм РЗиА для редактирования");
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
                    width:btnWidth,
                    align:"right",
                    click: async function (){
                        try{
                            
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

        let url = entitiesRepository.getRelayAlgorithmsUrl();
        entitiesRepository.getEntitiesInfo(url)
        .then(tableData => {
            let table = $$("relayAlgorithmTable_id");
            table.parse(tableData);
            table.refresh();
        })
        .catch(error => {
            messageBox.error(error.message);
        });
    }
}