class ProjectTableLayout {
    constructor(parentLayout) {
        this.parentLayout = parentLayout;
        this.tableLayout = {
            view: "datatable",
            id: "projectTable_id", 
            select: "row",
            adjust: true,
            resizeColumn: true,
            columns: [
                { id: "id",             adjust: true, header: ["GUID:", ""] },
                { id: "divg",           adjust: true, header: ["ДИВГ:", ""] },
                { id: "title",          adjust: true, header: ["Наименование:", { content: "multiSelectFilter" }] },
                { id: "version",        adjust: true, header: ["Версия:", ""] },
                { id: "status",        adjust: true, header: ["Статус:", { content: "multiSelectFilter" }] },
                // { id: "platform",       adjust: true, header: ["Платформа:", { content: "multiSelectFilter" }] },
                // { id: "module",         adjust: true, header: ["Аналоговый модуль:", { content: "multiSelectFilter" }] },
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
                            
                            let prjVers = new ProjectVersion();
                            prjVers.initialize();

                            let win = new ProjectEditWindow(prjVers, async function(data){
                                messageBox.information(await entitiesRepository.createEntity(urlProjects, data));

                                let urlPrj = entitiesRepository.getProjectsVersionsUrl();
                                let tableData = await entitiesRepository.getEntitiesInfo(urlPrj);
                                let table = $$("projectTable_id");
                                table.parse(tableData);
                                table.refresh();
                            });
                            win.show();

                            /*
                            // получить версию проекта по умолчанию:
                            let urlProjects = entitiesRepository.getProjectsVersionsUrl();
                            let projectDitales = await entitiesRepository.getDefaultEntity(urlProjects);

                            // получить все статусы которые принимает проект:
                            let statuses = await entitiesRepository.getProjectStatuses();

                            // получить все имеющиеся платформы:
                            let urlPlatform = entitiesRepository.getPlatformsUrl();
                            let platforms = await entitiesRepository.getEntitiesInfo(urlPlatform);
                                                        
                            let win = new ProjectEditWindow(projectDitales, statuses, platforms, async function(data){
                                messageBox.information(await entitiesRepository.createEntity(urlProjects, data));

                                let tableData = await entitiesRepository.getEntitiesInfo(urlProjects);
                                let table = $$("projectTable_id");
                                table.parse(tableData);
                                table.refresh();
                            });
                            win.show();
                            */
                        }
                        catch (error) {
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
                    css: "webix_transparent",
                    value: "Добавить редакцию БФПО",
                    width: 250,
                    align: "right",
                    click: async function () {
                        try {
                            
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

        let urlProject = entitiesRepository.getProjectsVersionsUrl();
        entitiesRepository.getEntitiesInfo(urlProject).then(tableData => {
            $$("projectTable_id").parse(tableData);
            $$("projectTable_id").refresh();
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