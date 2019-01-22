define("modules/clean/react/home/experiments/calendar/calendar_flyout_async",["require","exports","tslib","react"],function(e,t,n,a){"use strict";Object.defineProperty(t,"__esModule",{value:!0}),a=n.__importDefault(a);var o=(function(){function e(){}return e})();t.AsyncCalendarFlyoutState=o;var r=(function(t){function o(){var e=null!==t&&t.apply(this,arguments)||this;return e.state={},e}return n.__extends(o,t),o.prototype.componentDidMount=function(){var t=this;new Promise(function(t,n){e(["modules/clean/react/home/experiments/calendar/calendar_flyout"],t,n)}).then(n.__importStar).then(function(e){var n=e.CalendarFlyout;t.setState({fullCalendarComponent:n})})},o.prototype.render=function(){return this.state.fullCalendarComponent?a.default.createElement(this.state.fullCalendarComponent,this.props.data):null},o.displayName="AsyncCalendarFlyout",o})(a.default.PureComponent);t.AsyncCalendarFlyout=r}),define("modules/clean/react/maestro/pagelet_top_menu_container",["require","exports","tslib","react","external/react-dom","modules/clean/react/home/experiments/calendar/calendar_flyout_async","spectrum/vertically_fixed","modules/clean/react/account_menu/maestro_account_menu","modules/clean/react/maestro/header_conditions","modules/clean/react/user_notifications/dropdown","modules/clean/viewer","modules/clean/react/maestro/a11y_constants"],function(e,t,n,a,o,r,l,u,c,i,s,d){"use strict";Object.defineProperty(t,"__esModule",{value:!0}),a=n.__importDefault(a),o=n.__importStar(o);var m=(function(e){function t(){return null!==e&&e.apply(this,arguments)||this}return n.__extends(t,e),t.prototype.render=function(){var e=s.Viewer.get_viewer(),t=e.get_user_by_id(this.props.userId),n=c.showAccountMenu(e,t);return a.default.createElement(l.VerticallyFixed,{className:"pagelet-top-menu__container maestro-min-width"},a.default.createElement("div",{className:"pagelet-top-menu__notifications-button-container pagelet-top-menu__element",tabIndex:d.LAYOUT_TAB_ORDER.NOTIFICATION_BELL},a.default.createElement(i.UserNotificationsDropdown,{isPagelet:!0,displayBell:!t.is_guest_admin})),this.props.calendarProtoData.shouldShow&&a.default.createElement("div",{className:"pagelet-top-menu__calendar-button-container pagelet-top-menu__element",tabIndex:d.LAYOUT_TAB_ORDER.CALENDAR_FLYOUT},a.default.createElement(r.AsyncCalendarFlyout,{data:this.props.calendarProtoData})),a.default.createElement("div",{className:"pagelet-top-menu__account-button-container pagelet-top-menu__element",tabIndex:d.LAYOUT_TAB_ORDER.ACCOUNT_MENU},n&&a.default.createElement(u.MaestroAccountMenu,{user:t,viewer:e})))},t.prototype.componentDidMount=function(){setTimeout(function(e){window.ensemble&&window.ensemble.markPageletRenderedByDOMNode(e)},500,o.findDOMNode(this))},t})(a.default.Component);t.PageletTopMenuContainer=m});
//# sourceMappingURL=pkg-header.min.js-vfl3Nq7Ub.map