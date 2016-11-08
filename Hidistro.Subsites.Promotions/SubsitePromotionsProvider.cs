namespace Hidistro.Subsites.Promotions
{
    using Hidistro.Core.Entities;
    using Hidistro.Entities.Promotions;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Runtime.InteropServices;
    using Hidistro.Membership.Context;
    using Hidistro.Entities.Members;
    using Hidistro.Core;

    public abstract class SubsitePromotionsProvider
    {
        private static readonly SubsitePromotionsProvider _defaultInstance = (DataProviders.CreateInstance("Hidistro.Subsites.Data.PromotionData,Hidistro.Subsites.Data") as SubsitePromotionsProvider);

        protected SubsitePromotionsProvider()
        {
        }

        public abstract int AddBundlingProduct(BundlingInfo bind, DbTransaction dbTran);
        public abstract bool AddBundlingProductItems(int BundlingID, List<BundlingItemInfo> bundlingiteminfo, DbTransaction dbTran);
        public abstract bool AddCountDown(CountDownInfo countDownInfo);
        public abstract int AddGroupBuy(GroupBuyInfo groupBuy, DbTransaction dbTran);
        public abstract bool AddGroupBuyCondition(int groupBuyId, IList<GropBuyConditionInfo> gropBuyConditions, DbTransaction dbTran);
        public abstract int AddPromotion(PromotionInfo promotion, DbTransaction dbTran);
        public abstract bool AddPromotionMemberGrades(int activityId, IList<int> memberGrades, DbTransaction dbTran);
        public abstract bool AddPromotionProducts(int activityId, string productIds);
        public abstract CouponActionStatus CreateCoupon(CouponInfo coupon, int count, out string lotNumber);
        public abstract bool DeleteBundlingByID(int bundlingID, DbTransaction dbTran);
        public abstract bool DeleteBundlingProduct(int bundlingID);
        public abstract bool DeleteCountDown(int countDownId);
        public abstract bool DeleteCoupon(int couponId);
        public abstract bool DeleteGiftById(int giftId);
        public abstract bool DeleteGroupBuy(int groupBuyId);
        public abstract bool DeleteGroupBuyCondition(int groupBuyId, DbTransaction dbTran);
        public abstract bool DeletePromotion(int activityId);
        public abstract bool DeletePromotionProducts(int activityId, int? productId);
        public abstract bool DownLoadGift(GiftInfo giftinfo);
        public abstract bool EditPromotion(PromotionInfo promotion, DbTransaction dbTran);
        public abstract DbQueryResult GetAbstroGiftsById(GiftQuery query);
        public abstract BundlingInfo GetBundlingInfo(int bindID);
        public abstract DbQueryResult GetBundlingProducts(BundlingInfoQuery query);
        public abstract CountDownInfo GetCountDownInfo(int countDownId);
        public abstract DbQueryResult GetCountDownList(GroupBuyQuery query);
        public abstract CouponInfo GetCouponDetails(int couponId);
        public abstract IList<CouponItemInfo> GetCouponItemInfos(string lotNumber);
        public abstract DbQueryResult GetCouponsList(CouponItemInfoQuery componquery);
        public abstract decimal GetCurrentPrice(int groupBuyId, int prodcutQuantity);
        public abstract DbQueryResult GetGifts(GiftQuery query);
        public abstract GroupBuyInfo GetGroupBuy(int groupBuyId);
        public abstract DbQueryResult GetGroupBuyList(GroupBuyQuery query);
        public abstract IList<Member> GetMembersByRank(int? gradeId);
        public abstract GiftInfo GetMyGiftsDetails(int giftId);
        public abstract DbQueryResult GetNewCoupons(Pagination page);
        public abstract int GetOrderCount(int groupBuyId);
        public abstract string GetPriceByProductId(int productId);
        public abstract IList<MemberGradeInfo> GetPromoteMemberGrades(int activityId);
        public abstract PromotionInfo GetPromotion(int activityId);
        public abstract DataTable GetPromotionProducts(int activityId);
        public abstract DataTable GetPromotions(bool isProductPromote);
        public abstract List<int> GetUderlingIds(int? gradeId, string userName);
        public static SubsitePromotionsProvider Instance()
        {
            return _defaultInstance;
        }

        public abstract bool ProductCountDownExist(int productId);
        public abstract bool ProductGroupBuyExist(int productId);
        public abstract bool SendClaimCodes(int couponId, CouponItemInfo couponItem);
        public abstract bool SetGroupBuyEndUntreated(int groupBuyId);
        public abstract bool SetGroupBuyStatus(int groupBuyId, GroupBuyStatus status);
        public abstract void SwapCountDownSequence(int countDownId, int displaySequence);
        public abstract void SwapGroupBuySequence(int groupBuyId, int displaySequence);
        public abstract bool UpdateBundlingProduct(BundlingInfo bind, DbTransaction dbTran);
        public abstract bool UpdateCountDown(CountDownInfo countDownInfo);
        public abstract CouponActionStatus UpdateCoupon(CouponInfo coupon);
        public abstract bool UpdateGroupBuy(GroupBuyInfo groupBuy, DbTransaction dbTran);
        public abstract bool UpdateMyGifts(GiftInfo giftInfo);
    }
}

