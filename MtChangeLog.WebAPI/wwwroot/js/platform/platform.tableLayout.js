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
                            let platform = new Platform();
                            await platform.defaultInitialize();
                            platform.beforeEnding = async function(url, answer){
                                messageBox.information(answer);
                                
                                let tableData = await entitiesRepository.getEntitiesInfo(url);
                                let table = $$("platformTable_id");
                                table.parse(tableData);
                                table.refresh();
                            };

                            let win = new PlatformEditWindow(platform);
                            win.show();
                        } catch (error) {
                            messageBox.error(error.message);
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
                            let selected = $$("platformTable_id").getSelectedItem();
                            if(selected != undefined) {
                                let platform = new Platform();
                                await platform.initialize(selected);
                                platform.beforeEnding = async function(url, answer){
                                    messageBox.information(answer);
                                    
                                    let tableData = await entitiesRepository.getEntitiesInfo(url);
                                    let table = $$("platformTable_id");
                                    table.parse(tableData);
                                    table.refresh();
                                };

                                let win = new PlatformEditWindow(platform);
                                win.show();
                            } else {
                                messageBox.information("выберете платформу для редактирования");
                            }
                        } catch (error) {
                            messageBox.error(error.message);
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
                                messageBox.information(answer); 
                            } else {
                                messageBox.information("выберете платформу для удаления");
                            }
                        } catch (error) {
                            messageBox.error(error.message);  
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

        let url = entitiesRepository.getPlatformsUrl();
        entitiesRepository.getEntitiesInfo(url)
        .then(tableData => {
            let table = $$("platformTable_id");
            table.parse(tableData);
            table.refresh();
        })
        .catch(error => {
            messageBox.error(error.message);
        });
    }
}