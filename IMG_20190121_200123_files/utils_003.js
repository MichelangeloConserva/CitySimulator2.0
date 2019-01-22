define(["require","exports","tslib","modules/clean/ajax","modules/clean/api_v2/user_client","modules/core/browser","modules/core/browser_detection","modules/clean/sharing/api/client","modules/constants/file_viewer","modules/clean/file_store/utils","modules/clean/viewer","modules/clean/react/paper/paper_log","modules/core/i18n","external/lodash","modules/clean/react/bubble_dropdown_v2","modules/core/notify","modules/clean/react/size_class/constants","modules/clean/react/sign_in/utils"],function(e,r,n,t,o,i,a,s,l,c,u,d,p,_,f,m,b,h){"use strict";function g(e,r){var n=51200;return"CONTROL"===r?n=20971520:"ONE_HUNDRED_KB"===r?n=102400:"FIFTY_KB"===r&&(n=51200),e.bytes<=n}function P(e){var r=e.file,n=e.sharedLink;return A(e)?v(r)?n?N.Button:N.Dropdown:N.Ellipsis:N.Off}function T(e){return e===N.Dropdown||e===N.Ellipsis?f.BubbleDropdownPositions.BOTTOM_LEFT:f.BubbleDropdownPositions.BOTTOM}function A(e){var r=e.file,n=e.user,t=e.sharedLink;return!!r&&((v(r)&&S.includes(l.OPEN_IN_PAPER_VARIANT)||x(r)&&R.includes(l.EMBED_TO_PAPER_VARIANT))&&!u.Viewer.get_viewer().is_assume_user_session&&!a.is_mobile_or_tablet()&&a.is_supported_paper_browser()&&(!n&&!!t||!!n&&!n.is_paper_disabled))}function x(e){var r=c.getExtension(e);return!!r&&y.includes(r)}function v(e){var r=c.getExtension(e);return!!r&&(D.includes(r)&&e.bytes<20971520)}function k(e){var r=p._("Copy to Paper");return x(e)&&(r=p._("Add to Paper doc")),r}function w(e){return p._("Collect ideas and connect all kinds of files in a single doc.")}function C(e,r){return r===b.SizeClass.Large&&x(e)}function I(e){return x(e)?p._("Bring it all together"):p._("Bring this doc to life")}function O(e){return x(e)?p._("Add this file to a Paper doc to collect ideas and organize your thoughts."):p._("Copy to Paper and make changes or ask for feedback.")}function B(e){var r=e.file,n=e.sharedLink,o=e.contUrl,i=e.successCallback,a=e.errorCallback,s=e.extension,l=e._tk,c=e.oqa,u=e.fromModal;t.WebRequest({url:"/paper/sign_request_data",data:{path:r.file_id,sharedLink:n,cont_url:o,extension:s,_tk:l,oqa:c,fromModal:u},success:i,error:a})}function E(e){var r=e.file,n=e.user,t=e.fromModal;if(!n)return void i.redirect(h.getSignInAndContinueUrl());var o=window.open("","_blank"),a=function(e){o?i.redirect(e,o):i.redirect(e)},l=function(){o&&o.close(),m.Notify.error(p._("Can’t copy file to Paper"))},u=e.sharedLink?e.sharedLink:"",_=e.sharedLink?"shared_file_actions":"mounted_file_actions",f=c.getExtension(r);d.paperLog(d.PaperEventTypes.CTA_CLICK,{source:_,cta:"open_in_paper_button",extension:f});var b=f,g={file:r,sharedLink:u,contUrl:"/create-doc-from-dropbox-file",extension:f,successCallback:a,errorCallback:l,_tk:"dropbox_web_copy_from_previews",oqa:b,fromModal:t};if(v(r))B(g);else if(g._tk="dropbox_web_embed_from_previews",!u&&n){var P=new s.FileShareApiClient({userId:n.id});P.sharedLinkInfo({fileIdOrPath:r.file_id}).then(function(e){e?(g.sharedLink=e.url,g.contUrl="/dropbox-file-embed",B(g)):P.createSharedLink({fileIdOrPath:r.file_id}).then(function(e){g.sharedLink=e.url,g.contUrl="/dropbox-file-embed",B(g)}).catch(function(e){l&&l()})}).catch(function(e){l&&l()})}else g.contUrl="/dropbox-file-embed",B(g)}function L(e){return n.__awaiter(this,void 0,void 0,function(){return n.__generator(this,function(r){return[2,V().ns("paper_post_tti").rpc("paper_experiments",{},{subjectUserId:e.id}).then(function(e){return e.experiments})]})})}Object.defineProperty(r,"__esModule",{value:!0}),t=n.__importStar(t),i=n.__importStar(i),l=n.__importStar(l),_=n.__importStar(_);var N,S=["VARIANT_A","VARIANT_B1","VARIANT_B2","VARIANT_B3","VARIANT_B4","ON"],R=["BUTTON","DROPDOWN","ELLIPSIS"],D=["docx"],y=["jpg","jpeg","png","pdf","xls","xlsx","mp4","mov","pptx","ppt","zip","rar","mp3"];(function(e){e[e.Off=1]="Off",e[e.Control=2]="Control",e[e.Button=3]="Button",e[e.Dropdown=4]="Dropdown",e[e.Ellipsis=5]="Ellipsis"})(N=r.OpenInPaperButtonVariant||(r.OpenInPaperButtonVariant={})),r.fileSizeWithinExperimentLimits=g,r.getOpenInPaperButtonVariant=P,r.getCalloutPosition=T,r.shouldShowOpenInPaper=A,r.fileSupportsEmbedToPaper=x,r.fileSupportsCopyToPaper=v,r.getCTAText=k,r.getCTATextDescription=w,r.shouldShowCTADescription=C,r.getCalloutTitle=I,r.getCalloutMessage=O,r.createPaperAccountAndRedirect=B,r.openFileInPaper=E;var V=_.once(function(){return new o.UserApiV2Client});r.getPaperExperimentsPostTTI=L});
//# sourceMappingURL=utils.min.js-vfl5rIt-a.map