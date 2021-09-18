class AnalogModuleEditWindow {
    constructor(moduleDitales, platforms, submitFunс) {
        
        let _editableModule = moduleDitales;
        if(_editableModule == undefined || _editableModule == null) {
            throw new Error("не выбран аналоговый модуль для изменений");
        }

        let _platforms = platforms;
        if (_platforms == undefined || _platforms == null) {
            throw new Error("отсутствует перечень рлатформ модулей");
        }
        
        let _submitFunс = submitFunс;
        if(_submitFunс == undefined || _submitFunс == null) {
            throw new Error ("не определена функция отправки");
        }

        let _window;
        this.show = function () {
            webix.ready(function () {
                _window = webix.ui(window());
                _window.show();
            });
        };

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
                        label:"Аналоговый модуль:"
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
                            _window.close();
                        }
                    }
                ]
            };
            return result;
        };

        let windowLayout = function () {
            let lWidth = 180;
            let result = {
                view:"layout",
                id:"winLayout",
                rows:[
                    { 
                        view:"text", 
                        label:"Децимальный номер:", 
                        labelAlign:"right", 
                        labelWidth:lWidth,
                        value:_editableModule.divg,
                        attributes:{
                            maxlength:13,
                        }, 
                        on:{
                            onChange: function(newValue, oldValue, config){
                                _editableModule.divg = newValue;
                                webix.message("!!!");
                            }
                        }
                    },
                    { 
                        view:"text", 
                        label:"Наименование модуля:", 
                        labelAlign:"right",
                        labelWidth:lWidth,
                        value:_editableModule.title,
                        attributes:{
                            maxlength:8,
                        }, 
                        on:{
                            onChange: function(newValue, oldValue, config){
                                _editableModule.title = newValue;
                                webix.message("!!!");
                            }
                        }
                    },
                    { 
                        view:"text", 
                        label:"Номинальный ток:", 
                        labelAlign:"right",
                        labelWidth:lWidth,
                        value:_editableModule.nominalCurrent,
                        attributes:{
                            maxlength:2,
                        }, 
                        on:{
                            onChange: function(newValue, oldValue, config){
                                _editableModule.nominalCurrent = newValue;
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
                        value:_editableModule.description,
                        attributes:{
                            maxlength:200,
                        }, 
                        on:{
                            onChange: function(newValue, oldValue, config){
                                _editableModule.description = newValue;
                                webix.message("!!!");
                            }
                        }
                    },
                    {
                        view:"multicombo", 
                        id:"modulePlatforms_id",
                        label:"Модули:",
                        labelAlign:"right",
                        labelWidth:lWidth,  
                        value:_editableModule.platforms,
                        button:true,
                        suggest:{
                            body:{
                                data:_platforms,
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
                                let selected = $$("modulePlatforms_id").getValue({options:true});
                                _editableModule.platforms = selected.map(function(item) {
                                    return {
                                        id:item.id,
                                        title:item.title,
                                        description:item.description
                                    };
                                });
                                await _submitFunс(_editableModule);
                            } catch (error) {
                                webix.message({
                                    text: "[ERROR] - " + error.message,
                                    type: "error", 
                                    expire: 10000,
                                });
                            }
                            finally {
                                _window.close();
                            }
                        }
                    }
                ]
            };
            return result;
        };
    }
}