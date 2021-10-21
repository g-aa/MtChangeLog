class CommunicationTableLayout{
    constructor(parentLayout){
        this.parentLayout = parentLayout;
        this.tableLayout = {
            view:"datatable",
            id:"communicationTable_id", 
            select:"row",
            adjust:true,
            resizeColumn:true,
            columns:[
                { id:"id",          adjust:true,    header:["GUID:", ""] },
                { id:"protocols",   adjust:true,    header:["Протоколы:", { content:"multiSelectFilter" }] },
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
                            let communication = new Communication();
                            await communication.defaultInitialize();
                            communication.beforeEnding = async function(url, answer){
                                messageBox.information(answer);
                                
                                let tableData = await entitiesRepository.getEntitiesInfo(url);
                                let table = $$("communicationTable_id");
                                table.parse(tableData);
                                table.refresh();
                            };

                            let win = new CommunicationEditWindow(communication);
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
                            let selected = $$("communicationTable_id").getSelectedItem();
                            if(selected != undefined){
                                let communication = new Communication();
                                await communication.initialize(selected);
                                communication.beforeEnding = async function(url, answer){
                                    messageBox.information(answer);
                                
                                    let tableData = await entitiesRepository.getEntitiesInfo(url);
                                    let table = $$("communicationTable_id");
                                    table.parse(tableData);
                                    table.refresh();
                                };

                                let win = new CommunicationEditWindow(communication);
                                win.show();
                            } else{
                                messageBox.information("выберете перечень протоколов для редактирования");
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

        let url = entitiesRepository.getCommunicationsUrl();
        entitiesRepository.getEntitiesInfo(url)
        .then(tableData => {
            let table = $$("communicationTable_id");
            table.parse(tableData);
            table.refresh();
        })
        .catch(error => {
            messageBox.error(error.message);
        });
    }
}