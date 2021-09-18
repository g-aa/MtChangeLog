class AnalogModuleTableLayout {
  constructor(parentLayout) {
    this.parentLayout = parentLayout;
    this.tableLayout = {
      view: "datatable",
      id: "analogModuleTable_id", 
      select: "row",
      adjust: true,
      resizeColumn: true,
      columns: [
          { id: "id",             adjust: true, header: ["GUID:", ""] },
          { id: "title",          adjust: true, header: ["Наименование:", { content: "multiSelectFilter" }] },
          { id: "divg",           adjust: true, header: ["ДИВГ:", ""] },
          { id: "nominalCurrent", adjust: true, header: ["Номинальный ток:", { content: "multiSelectFilter" }] },
          { id: "description",    fillspace: true, header: ["Описание:", ""] }
      ],
      data: []
    };

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
              let urlModule = entitiesRepository.getAnalogModulesUrl();
              let moduleDitales = await entitiesRepository.getDefaultEntity(urlModule);

              let urlPlatform = entitiesRepository.getPlatformsUrl();
              let platforms = await entitiesRepository.getEntitiesInfo(urlPlatform);

              let win = new AnalogModuleEditWindow(moduleDitales, platforms, async function(data) {
                let urlModule = entitiesRepository.getAnalogModulesUrl();
                let answer = await entitiesRepository.createEntity(urlModule, data);
                
                let tableData = await entitiesRepository.getEntitiesInfo(urlModule);
                $$("analogModuleTable_id").parse(tableData);
                $$("analogModuleTable_id").refresh();
              });
              win.show();
            } catch (error) {
              webix.message( {
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
              let module = $$("analogModuleTable_id").getSelectedItem();
              if(module != undefined) {
                let urlModule = entitiesRepository.getAnalogModulesUrl();
                let moduleDitales = await entitiesRepository.getEntityDetails(urlModule, module);

                let urlPlatform = entitiesRepository.getPlatformsUrl();
                let platforms = await entitiesRepository.getEntitiesInfo(urlPlatform);

                let win = new AnalogModuleEditWindow(moduleDitales, platforms, async function(data) {
                  let urlModule = entitiesRepository.getAnalogModulesUrl();
                  let answer = await entitiesRepository.updateEntity(urlModule, data);
                
                  let tableData = await entitiesRepository.getEntitiesInfo(urlModule);
                  $$("analogModuleTable_id").parse(tableData);
                  $$("analogModuleTable_id").refresh();
                });
                win.show();
              }
              else {
                messageBox.warning("Не выбран модуль для редактирования");
                
                webix.message({
                    text: "[INFO] - не выбран модуль для редактирования",
                    expire: 10000,
                });
              }
            } catch (error) {
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
              let module = $$("analogModuleTable_id").getSelectedItem();
              if(module != undefined) {
                let urlModule = entitiesRepository.getAnalogModulesUrl();
                let answer = await entitiesRepository.deleteEntity(urlModule, module);  
              }
              else {
                webix.message({
                    text: "[INFO] - не выбран модуль для удаления", 
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

      let urlModule = entitiesRepository.getAnalogModulesUrl();
      entitiesRepository.getEntitiesInfo(urlModule).then(tableData => {
        $$("analogModuleTable_id").parse(tableData);
        $$("analogModuleTable_id").refresh();
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