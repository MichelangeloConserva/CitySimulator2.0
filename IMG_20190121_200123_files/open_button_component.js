define(["require","exports","tslib","external/lodash","react","external/classnames","modules/clean/react/file_viewer/logging","modules/clean/react/file_viewer/constants","modules/clean/react/file_viewer/open_button/coachmark_wrapper","modules/clean/react/file_viewer/open_button/utils","modules/clean/react/file_viewer/open_button/types","modules/clean/react/paper/open_in_paper_button","modules/clean/react/file_viewer/unity/with_unity","modules/clean/react/file_viewer/utils","modules/core/i18n","react-dom","modules/clean/react/paper/open_in_paper_callout_loader","modules/clean/react/paper/utils","modules/clean/react/size_class/constants","spectrum/button","spectrum/popover","modules/clean/react/sprite_div"],function(e,t,n,o,i,r,a,p,l,s,u,c,d,f,h,m,_,C,y,O,E,P){"use strict";Object.defineProperty(t,"__esModule",{value:!0}),o=n.__importStar(o),i=n.__importDefault(i),r=n.__importDefault(r),m=n.__importStar(m),P=n.__importDefault(P);var b=(I={},I[u.OpenButtonAction.DOWNLOAD]="open-button__download",I[u.OpenButtonAction.UNITY_FILE]="open-button__unity",I[u.OpenButtonAction.UNITY_FOLDER]="open-button__unity-folder",I[u.OpenButtonAction.OPEN_WITH]="open-button__open-with",I),v=(function(e){function t(){var t=null!==e&&e.apply(this,arguments)||this;return t.state={hasBeenClicked:!1},t.getCopyToPaperExperimentVariant=o.once(function(){C.getPaperExperimentsPostTTI(t.props.user).then(function(e){t.setState({copyToPaperExperimentVariant:e.paper_copy_to_paper_filesizes})})}),t.handleActionClick=function(e,n){t.handleClick(),t.props.onClick&&t.props.onClick(),e.handler();var o=f.getSplitButtonUserActionContext(n,t.props.location);a.logUserAction(e.userAction,o)},t.handleClick=function(){t.setState({hasBeenClicked:!0})},t}return n.__extends(t,e),t.prototype.componentDidMount=function(){this.checkCopyToPaperExperimentVariant()},t.prototype.componentDidUpdate=function(){this.checkCopyToPaperExperimentVariant()},t.prototype.checkCopyToPaperExperimentVariant=function(){C.fileSupportsCopyToPaper(this.props.file)&&this.getCopyToPaperExperimentVariant()},t.prototype.isOpenInPaperSupported=function(){return!!this.props.hasOpenInPaperSupport&&C.fileSizeWithinExperimentLimits(this.props.file,this.state.copyToPaperExperimentVariant)},t.prototype.render=function(){var e=this,t=this.props,n=t.file,o=t.buttonVariant,r=t.isUnityDisabled,a=t.isOpenWithDisabled,p=t.unityInfo,u=t.sizeClass,c=t.user,d=t.extraOptions,f=this.state.hasBeenClicked,h=s.getOpenOptions({file:n,hasOpenInPaperSupport:this.isOpenInPaperSupported(),isUnityDisabled:r,isOpenWithDisabled:a,unityInfo:p,extraOptions:d,user:c,isSlackEnabled:this.props.showGeminiActions});return i.default.createElement("div",{className:"open-button",ref:function(t){e.ref=m.findDOMNode(t)}},i.default.createElement(l.OpenButtonCoachmarkWrapper,{inDropdown:1!==h.length,file:n,user:c,shouldShowCoachmark:s.shouldShowCoachmark(h,p)&&!f},0===h.length?i.default.createElement(B,{buttonVariant:o}):1===h.length?i.default.createElement(D,{file:n,buttonVariant:o,onClick:this.handleActionClick,option:h[0],user:c}):i.default.createElement(g,{file:n,buttonVariant:o,onItemClick:this.handleActionClick,onTriggerClick:this.handleClick,options:h,user:c,sizeClass:u})),this.isOpenInPaperSupported()?i.default.createElement(_.OpenInPaperCalloutLoader,{file:n,parentHasBeenClicked:f,sizeClass:u,targetNode:this.ref,user:c,variant:C.OpenInPaperButtonVariant.Dropdown}):null)},t.defaultProps={isUnityDisabled:!1,isOpenWithDisabled:!1,justifyRight:!0,buttonVariant:"secondary"},t})(i.default.Component);t._OpenButton=v;var k=function(e){var t=e.option,n=e.file,o=e.user,r=t.type;return i.default.createElement(E.PopoverContentItem,{key:t.text,value:t},r===u.OpenButtonAction.OPEN_IN_PAPER?i.default.createElement(c.OpenInPaperButton,{file:n,user:o,inDropdown:!0,hideCallout:!0}):i.default.createElement(P.default,{group:"web",name:t.spriteName,text:t.text}))},g=(function(e){function t(){var t=null!==e&&e.apply(this,arguments)||this;return t.handlePopoverSelection=function(e,n){n&&(n.preventDefault(),n.stopPropagation()),t.props.onItemClick&&t.props.onItemClick(e,p.SplitButtonActionLocation.Split)},t}return n.__extends(t,e),t.prototype.render=function(){var e=this,t=this.props.sizeClass;return i.default.createElement(E.Popover,{onSelection:this.handlePopoverSelection},i.default.createElement(E.PopoverTrigger,null,i.default.createElement(O.Button,{className:r.default("download-button","control__button"),onClick:this.props.onTriggerClick,tagName:"span",variant:this.props.buttonVariant,size:t===y.SizeClass.Small?"large":"default"},h._("Open ▾"))),i.default.createElement(E.PopoverContent,{attachment:"right"},this.props.options.map(function(t){return i.default.createElement(k,{option:t,file:e.props.file,user:e.props.user})})))},t})(i.default.Component),D=(function(e){function t(){var t=null!==e&&e.apply(this,arguments)||this;return t.handleClick=function(e){e.preventDefault(),e.stopPropagation(),t.props.onClick&&t.props.onClick(t.props.option,p.SplitButtonActionLocation.Main)},t}return n.__extends(t,e),t.prototype.getButtonText=function(){switch(this.props.option.type){case u.OpenButtonAction.DOWNLOAD:return h._("Download");case u.OpenButtonAction.PREPARE_FOR_SIGNATURE:return h._("Prepare For Signature");default:return h._("Open")}},t.prototype.render=function(){var e=this.props.option,t=e.type;return t===u.OpenButtonAction.OPEN_IN_PAPER?i.default.createElement(c.OpenInPaperButton,{file:this.props.file,user:this.props.user,inDropdown:!1,hideCallout:!0}):i.default.createElement(O.Button,{className:"control__button "+b[t],variant:this.props.buttonVariant,onClick:this.handleClick},this.getButtonText())},t})(i.default.Component),B=function(e){var t=e.buttonVariant;return i.default.createElement(O.Button,{"aria-label":h._("Disabled open button"),className:"control__button",disabled:!0,variant:t})},S=function(e){return i.default.createElement(d.WithUnity,{file:e.file,isUnityDisabled:e.isUnityDisabled,user:e.user,render:function(t){return i.default.createElement(v,n.__assign({},e,{unityInfo:t}))}})};t.OpenButton=S;var I});
//# sourceMappingURL=open_button_component.min.js-vfl31dev-.map