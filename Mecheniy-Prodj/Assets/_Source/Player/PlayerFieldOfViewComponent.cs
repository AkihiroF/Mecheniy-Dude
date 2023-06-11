using _Source.Lighting;
using _Source.Services;
using _Source.SignalsEvents.UpgradesEvents;

namespace _Source.Player
{
    public class PlayerFieldOfViewComponent : FieldOfViewComponent
    {
        private void UpgradeAngle(float percent)
        {
            angleView += angleView * percent / 100;
            ParametersField.AngleView = angleView;
        }
        private void OnDestroy()
        {
            Signals.Get<OnUpgradeAngleVision>().RemoveListener(UpgradeAngle);
        }
    }
}