define(["require","exports","tslib","modules/clean/react/file_metadata/data/types","modules/clean/redux/types"],function(t,e,a,i,s){"use strict";function r(t,r){switch(void 0===t&&(t=e.initialState),r.type){case i.Actions.GetUploadInfoApiFetch:return a.__assign({},t,{apiStatus:s.ApiClientStatus.Fetch});case i.Actions.GetUploadInfoApiSuccess:return a.__assign({},r.payload.uploadInfo,{apiStatus:s.ApiClientStatus.Success});case i.Actions.GetUploadInfoApiError:return a.__assign({},t,{apiStatus:s.ApiClientStatus.Error,error:r.payload.error});default:return t}}Object.defineProperty(e,"__esModule",{value:!0}),e.initialState={uploadDate:"",displayName:"",error:void 0,apiStatus:void 0},e.fileMetadataReducer=r});
//# sourceMappingURL=reducer.min.js-vflvFgjUq.map