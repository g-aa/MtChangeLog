class PlatformTableLayout {
    constructor(parentLayout) {
        this.parentLayout = parentLayout; 
        this.tableLayout = {
            view: "datatable",
            id: "platformTable_id", 
            select: "row",
            adjust: true,
            resizeColumn: true,
            columns: [
                { id: "id",             adjust: true, header: ["GUID:", ""] },
                { id: "title",          adjust: true, header: ["Наименование:", { content: "multiSelectFilter" }] },
                { id: "description",    fillspace: true, header: ["Описание:", ""] }
            ],
            data: []
        }

        let btnWidth = 200;
        this.buttonsLayout = {
            view: "toolbar", 
            cols: [
                { 
                    view: "button", 
                    css: "webix_primary",
                    value: "Добавить", 
                    width: btnWidth,
                    click: async function () {
                        try {
                            let urlPlatform = entitiesRepository.getPlatformsUrl();
                            let platformDitales = await entitiesRepository.getDefaultEntity(urlPlatform);
                            
                            let urlModule = entitiesRepository.getAnalogModulesUrl();
                            let modules = await entitiesRepository.getEntitiesInfo(urlModule);

                            let win = new PlatformEditWindow(platformDitales, modules, async function(data) {
                                let urlPlatform = entitiesRepository.getPlatformsUrl();    
                                let answer = await entitiesRepository.createEntity(urlPlatform, data);
                                
                                let tableData = await entitiesRepository.getEntitiesInfo(urlPlatform);
                                $$("platformTable_id").parse(tableData);
                                $$("platformTable_id").refresh();
                            });
                            win.show();
                        }
                        catch (error) {
                            webix.message({
                                text: "[ERROR] - " + error.message,
                                type: "error", 
                                expire: 10000,
                            });
                        }
                    }
                },
                { 
                    view: "button", 
                    css: "webix_secondary",
                    value: "Редактировать", 
                    width: btnWidth,
                    click: async function () {
                        try {
                            let platform = $$("platformTable_id").getSelectedItem();
                            if(platform != undefined) {
                                let urlPlatform = entitiesRepository.getPlatformsUrl();
                                let platformDitales = await entitiesRepository.getEntityDetails(urlPlatform, platform);
                            
                                let urlModule = entitiesRepository.getAnalogModulesUrl();
                                let modules = await entitiesRepository.getEntitiesInfo(urlModule);

                                let win = new PlatformEditWindow(platformDitales, modules, async function(data) {
                                    let urlPlatform = entitiesRepository.getPlatformsUrl();    
                                    let answer = await entitiesRepository.createEntity(urlPlatform, data);
                                
                                    let tableData = await entitiesRepository.getEntitiesInfo(urlPlatform);
                                    $$("platformTable_id").parse(tableData);
                                    $$("platformTable_id").refresh();
                                });
                                win.show();
                            }
                            else {
                                webix.message({
                                    text: "[INFO] - платформа не выбрана для редактирования",
                                    expire: 10000,
                                });
                            }
                        } 
                        catch (error) {
                            webix.message({
                                text: "[ERROR] - " + error.message,
                                type: "error", 
                                expire: 10000,
                            });
                        }
                    }
                },
                { 
                    view: "button",
                    css: "webix_danger",
                    value: "Удалить",
                    width: btnWidth,
                    align: "right",
                    click: async function () {
                        try {
                            let platform = $$("platformTable_id").getSelectedItem();
                            if(platform != undefined) {
                                let urlPlatform = entitiesRepository.getPlatformsUrl();
                                let answer = await entitiesRepository.deleteEntity(urlPlatform, platform);  
                            }
                            else {
                                webix.message({
                                    text: "[INFO] - не выбрана платформа для удаления", 
                                    expire: 10000,
                                });
                            }
                        } 
                        catch (error) {
                            webix.message({
                                text: "[ERROR] - " + error.message,
                                type: "error", 
                                expire: 10000,
                            });  
                        }
                    } 
                }
            ]
        }
    }

    show() {
        webix.ui({
                view: "layout",
                rows: [ this.buttonsLayout, this.tableLayout ]
            }, 
            this.parentLayout.getChildViews()[0]);

        let urlPlatform = entitiesRepository.getPlatformsUrl();
        entitiesRepository.getEntitiesInfo(urlPlatform).then(tableData => {
            $$("platformTable_id").parse(tableData);
            $$("platformTable_id").refresh();
        })
        .catch(error => {
            webix.message({
                text: "[ERROR] - " + error.message,
                type: "error", 
                expire: 10000,
            });
        });
    }
}