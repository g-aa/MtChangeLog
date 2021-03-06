class ProjectEditWindow {
    constructor(editablePrjVers){
        let _editablePrjVers = editablePrjVers;
        if(!_editablePrjVers instanceof ProjectVersion){
            throw new Error("не выбран проект (БФПО) для работы");
        }

        let _uiWindow;
        this.show = function(){
            webix.ready(function (){
                _uiWindow = webix.ui(window());
                _uiWindow.show();
            });
        }

        // запуск прогрес бара при выполнении операций:
        let showProgress = function(){
            _uiWindow.disable();
            webix.extend(_uiWindow, webix.ProgressBar);
            _uiWindow.showProgress({ type:"icon" });
        }

        // остановка прогрес бара:
        let closeProgress = function(){
            _uiWindow.hideProgress();
            _uiWindow.enable();
        }

        let window = function (){
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
        
        let headLayout = function (){
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
                        click: function (){
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
                            onChange: async function(newValue, oldValue, config){
                                try{
                                    _editablePrjVers.setDivg(newValue);    
                                } catch (error){
                                    messageBox.warning(error.message);
                                }
                            }
                        }
                    },
                    { 
                        view:"text", 
                        label:"Префикс:", 
                        labelAlign:"right",
                        labelWidth:lWidth, 
                        value:_editablePrjVers.getPrefix(),
                        attributes:{
                            maxlength:16
                        }, 
                        on:{
                            onChange: async function(newValue, oldValue, config){
                                try{
                                    _editablePrjVers.setPrefix(newValue);
                                } catch (error){
                                    messageBox.warning(error.message);
                                }
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
                            maxlength:16
                        }, 
                        on:{
                            onChange: async function(newValue, oldValue, config){
                                try{
                                    _editablePrjVers.setTitle(newValue);
                                } catch (error){
                                    messageBox.warning(error.message);
                                }
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
                            onChange: async function(newValue, oldValue, config){
                                try{
                                    _editablePrjVers.setVersion(newValue);
                                } catch (error){
                                    messageBox.warning(error.message);
                                }
                            }
                        }
                    },
                    {
                        view:"richselect",
                        id:"status_id",
                        label:"Статус:",
                        icon:"mdi mdi-list-status",
                        labelAlign:"right",
                        labelWidth:lWidth,
                        value:_editablePrjVers.getStatus(),
                        options:{
                            body:{
                               template:"#title#"
                            },   
                            data:_editablePrjVers.getStatuses()
                        },
                        on:{
                            onChange: async function(newValue, oldValue, config){
                                try{
                                    _editablePrjVers.setStatus(newValue);
                                } catch (error){
                                    messageBox.warning(error.message);
                                }
                            }
                        }
                    },
                    {
                        view:"richselect",
                        id:"platform_id",
                        label:"Платформа:",
                        icon:"mdi mdi-application-cog-outline",
                        labelAlign:"right",
                        labelWidth:lWidth,
                        value:_editablePrjVers.getPlatform(),
                        options:{
                            body:{
                               template:"#title#"
                            },   
                            data:_editablePrjVers.getPlatforms()
                        },
                        on:{
                            onChange: async function(newValue, oldValue, config){
                                let combo = $$("analogModule_id");
                                try {
                                    showProgress();
                                    await _editablePrjVers.setPlatform(newValue);
                                    combo.define("options",{
                                        body:{
                                            template:"#title#"
                                        },   
                                        data:_editablePrjVers.getAnalogModules()
                                    });
                                    combo.setValue(_editablePrjVers.getAnalogModule())
                                } catch (error) {
                                    combo.define("options", [ ]);
                                    messageBox.warning(error.message);
                                }
                                finally {
                                    combo.refresh();
                                    closeProgress();
                                }
                            }
                        }
                    },
                    {
                        view:"richselect",
                        id:"analogModule_id",
                        label:"Аналоговый модуль:",
                        icon:"mdi mdi-puzzle",
                        labelAlign:"right",
                        labelWidth:lWidth, 
                        value:_editablePrjVers.getAnalogModule(),
                        options:{
                            body:{
                               template:"#title#"
                            },   
                            data:_editablePrjVers.getAnalogModules()
                        },
                        on:{
                            onChange: async function(newValue, oldValue, config){
                                try{
                                    _editablePrjVers.setAnalogModule(newValue);
                                } catch (error){
                                    messageBox.warning(error.message);
                                }
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
                            maxlength:500
                        }, 
                        on:{
                            onChange: function(newValue, oldValue, config){
                                try{
                                    _editablePrjVers.setDescription(newValue);
                                } catch (error){
                                    messageBox.warning(error.message);
                                }
                            }
                        }
                    },
                    {
                        view:"button",
                        id:"submitButton_id",
                        value:"Отправить",
                        css:"webix_primary",
                        inputWidth:200,
                        align:"right",
                        click: async function(){
                            try{ 
                                showProgress();
                                // отправить:
                                await _editablePrjVers.submit();

                                // автоматически закрывать при удачном стечении обстоятельств:
                                _uiWindow.close();
                            } catch (error){
                                messageBox.alertWarning(error.message);
                            } finally{
                                closeProgress();
                            }
                        }
                    }
                ]
            };
            return result;
        };
    }
}