class ProjectRevisionLayout{
    constructor(parentLayout){
        this.parentLayout = parentLayout;

        let tableId = "projectRevisionTable_id";
        let refresh = async function(){
            let url = entitiesRepository.getProjectsRevisionsUrl();
            let tableData = await entitiesRepository.getEntitiesInfo(url);
            let table = $$(tableId);
            table.parse(tableData);
            table.refresh();
        }
        this.refresh = refresh;

        this.tableLayout = {
            view:"datatable",
            id:tableId,
            select:"row",
            adjust:true,
            resizeColumn:true,
            columns:[
                // { id: "id",         adjust: true,       header: ["GUID:", ""] },
                { id: "module",     adjust: true,       header: ["Аналоговый модуль:", { content: "multiSelectFilter" }] },
                { id: "title",      adjust: true,       header: ["Наименование:", { content: "multiSelectFilter" }] },
                { id: "version",    adjust: true,       header: ["Версия:", { content: "multiSelectFilter" }] },
                { id: "revision",   adjust: true,       header: ["Ревизия:", ""] },
                { id: "date",       adjust: true,       header: ["Дата:", ""] },
                { id: "armEdit",    adjust: true,       header: ["ArmEdit:", { content: "multiSelectFilter" }] },
                { id: "reason",     fillspace: true,    header: ["Причина:", ""] },
            ],
            data:[]
        }

        let btnWidth = 200;
        this.buttonsLayout = {
            view:"toolbar", 
            cols:[
                { 
                    view:"button", 
                    css:"webix_secondary",
                    value:"Редактировать", 
                    width:btnWidth,
                    click: async function (){
                        try{
                            let selected = $$(tableId).getSelectedItem();
                            if(selected != undefined) {
                                let revision = new ProjectRevision();
                                await revision.initialize(selected);
                                revision.beforeEnding = async function(url, answer){
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
            view: "layout",
            rows: [ this.buttonsLayout, this.tableLayout ]
        }, 
        this.parentLayout.getChildViews()[0]);
        this.refresh().catch(error => {
            messageBox.error(error.message);
        });
    }
}