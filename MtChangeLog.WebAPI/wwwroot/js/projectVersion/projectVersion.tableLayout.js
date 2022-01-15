class ProjectVersionTableLayout{
    constructor(){
        let tableLayputId = "projectVersionTable_id";
        this.tableLayout = {
            view:"datatable",
            id:tableLayputId, 
            select:"row",
            adjust:true,
            resizeColumn:true,
            scroll:"xy",
            columns:[
                //{ id:"id",          adjust:true, header:["GUID:", ""] },
                { id:"divg",        adjust:true, header:["ДИВГ:", ""] },
                { id:"module",      width:150, header:["Аналоговый модуль:", { content:"multiSelectFilter" }] },
                { id:"title",       width:150, header:["Наименование:", { content:"multiSelectFilter" }] },
                { id:"version",     width:150, header:["Версия:", { content:"multiSelectFilter" }] },
                { id:"status",      width:150, header:["Статус:", { content:"multiSelectFilter" }] },
                { id:"platform",    width:150, header:["Платформа:", { content:"multiSelectFilter" }] },
                { id:"description", adjust:true,    header:["Описание:", ""] },
                { fillspace:true }
            ],
            data:[]
        }

        // обновление таблицы:
        let refresh = async function(){
            let tableData = await repository.getTableProjectVersions();
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
                            let prjVers = new ProjectVersion();
                            await prjVers.defaultInitialize();
                            prjVers.beforeEnding = async function(answer){
                                await refresh();
                                messageBox.information(answer);
                            }
                            let win = new ProjectEditWindow(prjVers);
                            win.show();
                        } catch (error) {
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
                            if(selected != undefined) {
                                let prjVers = new ProjectVersion();
                                await prjVers.initialize(selected);
                                prjVers.beforeEnding = async function(answer){
                                    await refresh();
                                    messageBox.information(answer);
                                }
                                let win = new ProjectEditWindow(prjVers);
                                win.show();
                            } else{
                                messageBox.information("выберете проект для редактирования");
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
                                let answer = await repository.deleteProjectVersion(selected);
                    
                                // удалить обьект в UI части:
                                $$(tableLayputId).remove(selected.id);
                                messageBox.information(answer);
                            } else{
                                messageBox.information("выберете проект для удаления");
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
                    css:"webix_transparent",
                    value:"Добавить редакцию БФПО",
                    width:250,
                    align:"right",
                    click: async function (){
                        try{
                            mainLayout.showProgress();
                            let selected = $$(tableLayputId).getSelectedItem();
                            if(selected != undefined) {
                                let revision = new ProjectRevision();
                                await revision.defaultInitialize(selected);
                                revision.beforeEnding = async function(answer){
                                    messageBox.information(answer);
                                };
                                let win = new ProjectRevisionEditWindow(revision);
                                win.show();
                            } else{
                                messageBox.information("выберете проект для добавления редакции");
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
            view: "layout",
            rows: [ this.buttonsLayout, this.tableLayout ]
        }, 
        parentLayout.getChildViews()[0]);
        await this.refresh();
    }
}