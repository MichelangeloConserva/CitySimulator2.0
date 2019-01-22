define(["require","exports","tslib","modules/clean/react/file_sidebar/store/file_activity/selectors","modules/clean/react/file_sidebar/store/file_activity/api","modules/clean/react/file_sidebar/file_sidebar_logger"],function(e,t,i,r,n,a){"use strict";function c(e){return{type:"FILE_ACTIVITY|RECEIVE_STREAM_ERROR",error:e}}function o(e,t){return{type:"FILE_ACTIVITY|RECEIVE_FILE_ACTIVITY",activities:e,enabled:t}}function u(e,t){return{type:"FILE_ACTIVITY|LOADING_START",userId:e,file:t}}function l(e){return{type:"FILE_ACTIVITY|LOADING_END",state:e}}function f(){return{type:"FILE_ACTIVITY|RESET"}}function I(){return{type:"FILE_ACTIVITY|RESET_FILE_STATE"}}function _(e,t){return(e.file?e.file.file_id:-1)!==(t.file?t.file.file_id:-1)||e.userId!==t.userId}function s(e,i){return function(n,f){if(_({file:i,userId:e},r.getContext(f()))){var s=t.api.create(e);n(I()),n(u(e,i));var d=Date.now(),T=++E,A=i.file_id;return s.getActivityStream(A).then(function(t){var c=t.enabled,u=t.activity_group;a.logGetActivityStreamTiming(Date.now()-d,!0),_({file:i,userId:e},r.getContext(f()))||n(o(u,c))}).catch(function(e){a.logGetActivityStreamTiming(Date.now()-d,!1),T===E&&n(c(e))}).then(function(){T===E&&n(l(r.getState(f())))})}}}Object.defineProperty(t,"__esModule",{value:!0}),r=i.__importStar(r),t.api={create:function(e){return new n.FileActivityStreamApi(e)}},t.receiveStreamError=c,t.receiveFileActivity=o,t.loadingStart=u,t.loadingEnd=l,t.reset=f,t.resetFileState=I;var E=0;t.getActivityStream=s});
//# sourceMappingURL=actions.min.js-vflauSCAX.map