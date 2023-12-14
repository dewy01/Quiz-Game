using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows;
using System.Windows.Media;

namespace Quiz_Gui_Game.Animations
{
    public static class PageTransition
    {
        public static void FadeIn(UIElement element, double duration)
        {
            DoubleAnimation fadeInAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(duration)
            };

            Storyboard.SetTargetProperty(fadeInAnimation, new PropertyPath(UIElement.OpacityProperty));
            Storyboard.SetTarget(fadeInAnimation, element);

            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(fadeInAnimation);

            storyboard.Begin();
        }

        public static void FadeOut(UIElement element, double duration)
        {
            DoubleAnimation fadeOutAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(duration)
            };

            Storyboard.SetTargetProperty(fadeOutAnimation, new PropertyPath(UIElement.OpacityProperty));
            Storyboard.SetTarget(fadeOutAnimation, element);

            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(fadeOutAnimation);

            storyboard.Begin();
        }

        public static async Task SlideInAsync(UIElement element, double duration, bool fromLeft = true)
        {
            DoubleAnimation slideInAnimation = new DoubleAnimation
            {
                From = fromLeft ? -element.RenderSize.Width : element.RenderSize.Width,
                To = 0,
                Duration = TimeSpan.FromSeconds(duration)
            };

            await ApplyAnimationAsync(element, slideInAnimation, "(UIElement.RenderTransform).(TranslateTransform.X)");
        }

        public static async Task SlideOutAsync(UIElement element, double duration, bool toLeft = true)
        {
            DoubleAnimation slideOutAnimation = new DoubleAnimation
            {
                From = 0,
                To = toLeft ? -element.RenderSize.Width : element.RenderSize.Width,
                Duration = TimeSpan.FromSeconds(duration)
            };

            await ApplyAnimationAsync(element, slideOutAnimation, "(UIElement.RenderTransform).(TranslateTransform.X)");
        }

        private static Task ApplyAnimationAsync(UIElement element, AnimationTimeline animation, string targetProperty = "Opacity")
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

            void AnimationCompleted(object sender, EventArgs e)
            {
                element.RenderTransform = new TranslateTransform();
                tcs.SetResult(true);
            }

            animation.Completed += AnimationCompleted;

            Storyboard.SetTargetProperty(animation, new PropertyPath(targetProperty));
            Storyboard.SetTarget(animation, element);

            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(animation);

            storyboard.Begin();

            return tcs.Task;
        }


    }
}
