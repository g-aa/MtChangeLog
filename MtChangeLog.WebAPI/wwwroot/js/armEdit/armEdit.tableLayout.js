class ArmEditTableLayout{
    constructor(parentLayout){
        this.parentLayout = parentLayout;
        this.tableLayout = {
            view:"datatable",
            id:"armEditTable_id", 
            select:"row",
            adjust:true,
            resizeColumn:true,
            columns:[
                { id:"id",             adjust:true, header:["GUID:", ""] },
                { id:"divg",           adjust:true, header:["ДИВГ:", { content:"multiSelectFilter" }] },
                { id:"version",        adjust:true, header:["Версия:", { content:"multiSelectFilter" }] },
                { id:"date",           adjust:true, header:["Дата релиза:", { content:"multiSelectFilter" }] },
                { id:"description",    fillspace:true, header:["Описание:", ""] }
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
                            let armEdit = new ArmEdit();
                            await armEdit.defaultInitialize();
                            armEdit.beforeEnding = async function(url, answer){
                                messageBox.information(answer);
                                
                                let tableData = await entitiesRepository.getEntitiesInfo(url);
                                let table = $$("armEditTable_id");
                                table.parse(tableData);
                                table.refresh();
                            };

                            let win = new ArmEditWindow(armEdit);
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
                            let selected = $$("armEditTable_id").getSelectedItem();
                            if(selected != undefined){
                                let armEdit = new ArmEdit();
                                await armEdit.initialize(selected);
                                armEdit.beforeEnding = async function(url, answer){
                                    messageBox.information(answer);
                                
                                    let tableData = await entitiesRepository.getEntitiesInfo(url);
                                    let table = $$("armEditTable_id");
                                    table.parse(tableData);
                                    table.refresh();
                                };

                                let win = new ArmEditWindow(armEdit);
                                win.show();
                            } else{
                                messageBox.information("выберете ArmEdit для редактирования");
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

        let url = entitiesRepository.getArmEditsUrl();
        entitiesRepository.getEntitiesInfo(url)
        .then(tableData => {
            let table = $$("armEditTable_id");
            table.parse(tableData);
            table.refresh();
        })
        .catch(error => {
            messageBox.error(error.message);
        });
    }
}