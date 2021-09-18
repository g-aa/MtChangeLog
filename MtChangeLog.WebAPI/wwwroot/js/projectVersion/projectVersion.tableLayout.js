class ProjectTableLayout{
    constructor(parentLayout){
        this.parentLayout = parentLayout;
        this.tableLayout = {
            view:"datatable",
            id:"projectTable_id", 
            select:"row",
            adjust:true,
            resizeColumn:true,
            columns:[
                { id: "id",             adjust: true, header: ["GUID:", ""] },
                { id: "divg",           adjust: true, header: ["ДИВГ:", ""] },
                { id: "title",          adjust: true, header: ["Наименование:", { content: "multiSelectFilter" }] },
                { id: "version",        adjust: true, header: ["Версия:", ""] },
                { id: "status",         adjust: true, header: ["Статус:", { content: "multiSelectFilter" }] },
                // { id: "platform",       adjust: true, header: ["Платформа:", { content: "multiSelectFilter" }] },
                // { id: "module",         adjust: true, header: ["Аналоговый модуль:", { content: "multiSelectFilter" }] },
                { id: "description",    fillspace: true, header: ["Описание:", ""] }
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
                            let prjVers = new ProjectVersion();
                            await prjVers.defaultInitialize();
                            prjVers.beforeEnding = async function(url, answer){
                                messageBox.information(answer);
                                
                                let tableData = await entitiesRepository.getEntitiesInfo(url);
                                let table = $$("projectTable_id");
                                table.parse(tableData);
                                table.refresh();
                            }

                            let win = new ProjectEditWindow(prjVers);
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
                    width:btnWidth,
                    click: async function (){
                        try{
                            let selected = $$("projectTable_id").getSelectedItem();
                            if(selected != undefined) {
                                let prjVers = new ProjectVersion();
                                await prjVers.initialize(selected);
                                prjVers.beforeEnding = async function(url, answer){
                                    messageBox.information(answer);
                                
                                    let tableData = await entitiesRepository.getEntitiesInfo(url);
                                    let table = $$("projectTable_id");
                                    table.parse(tableData);
                                    table.refresh();
                                }

                                let win = new ProjectEditWindow(prjVers);
                                win.show();
                            } else{
                                messageBox.information("выберете проект для редактирования");
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
                },
                {
                    view:"button",
                    css:"webix_transparent",
                    value:"Добавить редакцию БФПО",
                    width:250,
                    align:"right",
                    click: async function (){
                        try{
                            let selected = $$("projectTable_id").getSelectedItem();
                            if(selected != undefined) {

                                let win = new ProjectRevisionEditWindow();
                                win.show();
                            } else{
                                messageBox.information("выберете проект для добавления редакции");
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

        let urlProject = entitiesRepository.getProjectsVersionsUrl();
        entitiesRepository.getEntitiesInfo(urlProject).then(tableData => {
            let table = $$("projectTable_id");
            table.parse(tableData);
            table.refresh();
        })
        .catch(error => {
            messageBox.error(error.message);
        });
    }
}