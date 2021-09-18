class ProjectEditWindow {
    constructor(projectDitales, statuses, platforms, submitFunс) {
        
        let _editablePrjVers = null;
        if(_editablePrjVers == undefined || _editablePrjVers == null) {
            throw new Error("не выбран проект для изменений");
        }

        /*
        let _editableProject = projectDitales;
        if(_editableProject == undefined || _editableProject == null) {
            throw new Error("не выбран проект для изменений");
        }

        let _platforms = platforms;
        if (_platforms == undefined || _platforms == null) {
            throw new Error("отсутствует перечень платформ");
        }
        let _analogModules = [];

        if (statuses == undefined || statuses == null) {
            throw new Error("отсутствует перечень статусов для проекта");
        }

        let _submitFunс = submitFunс;
        if(_submitFunс == undefined || _submitFunс == null) {
            throw new Error ("не определена функция отправки");
        }
        */
        let _uiWindow;
        this.show = function() {
            webix.ready(function () {
                _uiWindow = webix.ui(window());
                _uiWindow.show();
            });
        }

        let window = function () {
            let result = {
                view:"window",
                id:"win_id",
                modal:true,
                move:true,
                width:600,
                height:600,
		        position:"center",
                head:headLayout(),
                body:windowLayout()
            };
            return result;
        };
        
        let headLayout = function () {
            let result = {
                view:"toolbar",
                id:"headToolbar",
                height:40,
                elements:[
                    {
                        view:"label",
                        label:"Проект:"
                    },
                    {
                        
                    },
                    {
                        view:"icon",
                        icon:"wxi-close",
                        id:"btn_oscWinClose",
                        align:"right",
                        tooltip:"закрыть окно",
                        click: function () {
                            _uiWindow.close();
                        }
                    }
                ]
            };
            return result;
        };

        let windowLayout = function () {
            let lWidth = 160;
            let result = {
                view:"layout",
                id:"winLayout",
                rows:[
                    { 
                        view:"text", 
                        label:"Децимальный номер:", 
                        labelAlign:"right",
                        labelWidth:lWidth,
                        value:_editablePrjVers.getDivg(),
                        attributes:{
                            maxlength:13
                        }, 
                        on:{
                            onChange: function(newValue, oldValue, config){
                                _editablePrjVers.setDivg(newValue)
                                // _editableProject.divg = newValue;
                            }
                        }
                    },
                    { 
                        view:"text", 
                        label:"Наименование:", 
                        labelAlign:"right",
                        labelWidth:lWidth, 
                        value:_editablePrjVers.getTitle(),
                        attributes:{
                            maxlength:5
                        }, 
                        on:{
                            onChange: function(newValue, oldValue, config) {
                                _editablePrjVers.setTitle(newValue);
                                // _editableProject.title = newValue;
                            }
                        }
                    },
                    {
                        view:"text", 
                        label:"Версия:", 
                        labelAlign:"right",
                        labelWidth:lWidth,
                        value:_editablePrjVers.getVersion(),
                        attributes:{
                            maxlength:2
                        }, 
                        on:{
                            onChange: function(newValue, oldValue, config){
                                _editablePrjVers.setVersion(newValue);
                                // _editableProject.version = newValue;
                            }
                        }
                    },
                    {
                        view:"combo",
                        id:"status_id",
                        label:"Статус:",
                        labelAlign:"right",
                        labelWidth:lWidth,
                        value:_editablePrjVers.getStatus(),
                        options:_editablePrjVers.getStatuses(),
                        on:{
                            onChange: function(newValue, oldValue, config){
                                _editablePrjVers.setStatus(newValue);
                                // _editableProject.status = newValue;
                            }
                        }
                    },
                    {
                        view:"combo",
                        id:"platform_id",
                        label:"Платформа:",
                        labelAlign:"right",
                        labelWidth:lWidth,
                        options:{
                            body: {
                               template:"#title#"
                            },   
                            data:_editablePrjVers.getPlatforms()
                        },
                        on:{
                            onChange: async  function(newValue, oldValue, config){
                                let combo = $$("analogModule_id");
                                try {
                                    
                                    _editablePrjVers.setPlatform(newValue);
                                    
                                    combo.define("options", {
                                        body:{
                                            template:"#title#"
                                        },   
                                        data:_editablePrjVers.getAnalogModules()
                                    });


                                    if (newValue != undefined && newValue != null && newValue != "") {
                                        let urlPlatform = entitiesRepository.getPlatformsUrl();
                                        let temp = { id:newValue };
                                        let platform = await entitiesRepository.getEntityDetails(urlPlatform, temp);
                                        
                                        _editableProject.platform = _platforms.find(function(item, index, array){
                                            return item.id == newValue;
                                        });
                                        _analogModules = platform.analogModules;
                                        
                                        combo.define("options", {
                                            body:{
                                                template:"#title#"
                                            },   
                                            data:_analogModules
                                        });
                                    }
                                } catch (error) {
                                    combo.define("options", [ ]);
                                    webix.message({
                                        text: "[ERROR] - " + error.message,
                                        type: "error", 
                                        expire: 10000,
                                    });
                                }
                                finally {
                                    combo.refresh();
                                }
                            }
                        }
                    },
                    {
                        view:"combo",
                        id:"analogModule_id",
                        label:"Аналоговый модуль:",
                        labelAlign:"right",
                        labelWidth:lWidth, 
                        value:"",
                        options:_editablePrjVers.getAnalogModules(),
                        on:{
                            onChange: async  function(newValue, oldValue, config){
                                _editablePrjVers.setAnalogModule(newValue);
                                //_editableProject.analogModule = _analogModules.find(function(item, index, array){
                                //    return item.id == newValue;
                                //});
                            }
                        }
                    },
                    {
                        view:"textarea",
                        label:"Описание:",
                        labelAlign:"right",
                        labelWidth:lWidth, 
                        height:120, 
                        value:_editablePrjVers.getDescription(),
                        attributes:{
                            maxlength:255
                        }, 
                        on:{
                            onChange: function(newValue, oldValue, config) {
                                _editablePrjVers.setDescription(newValue);
                                // _editableProject.description = newValue;
                            }
                        }
                    },
                    {
                        view:"button",
                        id:"submitButton_id",
                        value:"Сохронить",
                        css:"webix_primary",
                        inputWidth:200,
                        align:"right",
                        click: async function () {
                            try { 
                                await _submitFunс(_editableProject);
                            } 
                            catch (error) {
                                webix.message({
                                    text: "[ERROR] - " + error.message,
                                    type: "error", 
                                    expire: 10000,
                                });
                            }
                            finally {
                                _uiWindow.close();
                            }
                        }
                    }
                ]
            };
            return result;
        };
    }
}