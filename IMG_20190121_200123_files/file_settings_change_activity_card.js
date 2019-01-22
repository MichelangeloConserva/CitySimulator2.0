define(["require","exports","tslib","react","modules/clean/react/file_activity_stream/file_activity_stream_card","modules/clean/react/file_activity_stream/utils","modules/clean/react/file_viewer/share_helpers","modules/clean/react_format","modules/core/i18n"],function(e,t,r,i,n,s,o,a,l){"use strict";Object.defineProperty(t,"__esModule",{value:!0}),i=r.__importDefault(i),o=r.__importStar(o);var c=(function(e){function t(){var t=null!==e&&e.apply(this,arguments)||this;return t.onShareSettingsClick=function(){o.share(t.props.file,t.props.viewingUser,null,"PREVIEW_PAGE")},t}return r.__extends(t,e),Object.defineProperty(t.prototype,"message",{get:function(){var e,t=this.props.activity,r=t.user,n=t.changed_viewer_info_policy;return e="enabled"===n?s.isViewingUser(this.props)?l._("<strong>You</strong> turned on viewer info"):l._("<strong>%(name)s</strong> turned on viewer info"):"disabled"===n?s.isViewingUser(this.props)?l._("<strong>You</strong> turned off viewer info"):l._("<strong>%(name)s</strong> turned off viewer info"):s.isViewingUser(this.props)?l._("<strong>You</strong> changed the file settings"):l._("<strong>%(name)s</strong> changed the file settings"),a.reactFormat(e,{strong:i.default.createElement("strong",null),name:r.display_name})},enumerable:!0,configurable:!0}),t.prototype.render=function(){var e=this.props.activity;return i.default.createElement(n.FileActivityStreamCard,{user:e.user,timestamp:e.timestamp,action:{text:l._("Sharing settings"),onClick:this.onShareSettingsClick},message:this.message})},t})(i.default.Component);t.FileSettingsChangeActivityCard=c});
//# sourceMappingURL=file_settings_change_activity_card.min.js-vflyBOvvH.map