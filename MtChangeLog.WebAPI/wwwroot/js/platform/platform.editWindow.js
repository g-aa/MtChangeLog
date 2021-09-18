class PlatformEditWindow {
    constructor(platformDitales, analogModules, submitFunс) {

        let _editablePlatform = platformDitales;
        if(_editablePlatform == undefined || _editablePlatform == null) {
            throw new Error("не выбрана платформа для изменений");
        }

        let _analogModules = analogModules;
        if (_analogModules == undefined || _analogModules == null) {
            throw new Error("отсутствует перечень аналоговых модулей");
        }
        
        let _submitFunс = submitFunс;
        if(_submitFunс == undefined || _submitFunс == null) {
            throw new Error ("не определена функция отправки");
        }

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
                height:400,
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
                        label:"Платформа:"
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
            let lWidth = 100;
            let result = {
                view:"layout",
                id:"winLayout",
                rows:[
                    { 
                        view:"text", 
                        label:"Платформа:", 
                        labelAlign:"right",
                        labelWidth:lWidth,
                        value:_editablePlatform.title,
                        attributes:{
                            maxlength:10
                        }, 
                        on:{
                            onChange: function(newValue, oldValue, config){
                                _editablePlatform.title = newValue;
                                webix.message("!!!");
                            }
                        }
                    },
                    { 
                        view:"textarea", 
                        label:"Описание:", 
                        labelAlign:"right",
                        labelWidth:lWidth, 
                        height:150, 
                        value:_editablePlatform.description,
                        attributes:{
                            maxlength:255
                        }, 
                        on:{
                            onChange: function(newValue, oldValue, config){
                                _editablePlatform.description = newValue;
                                webix.message("!!!");
                            }
                        }
                    },
                    {
                        view:"multicombo", 
                        id:"platformAnalogModules_id",
                        label:"Модули:",
                        labelAlign:"right",
                        labelWidth:lWidth,  
                        value:_editablePlatform.analogModules,
                        button:true,
                        suggest:{
                            body:{
                                data:_analogModules,
                                template:webix.template("#title#")
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
                                let selected = $$("platformAnalogModules_id").getValue({options:true});
                                _editablePlatform.analogModules = selected.map(function(item) {
                                    return {
                                        id:item.id,
                                        title:item.title,
                                        divg:item.divg,
                                        nominalCurrent:item.nominalCurrent,
                                        description:item.description
                                    };
                                });
                                await _submitFunс(_editablePlatform);
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