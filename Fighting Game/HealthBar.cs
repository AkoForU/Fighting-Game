using System;
using System.Drawing;
using System.Windows.Forms;

public class HealthBar : Control
{
    private int health = 100;

    public int Health
    {
        get => health;
        set
        {
            health = Math.Max(0, Math.Min(100, value)); // Clamp between 0 and 100
            Invalidate(); // Redraw the control
        }
    }

    public HealthBar()
    {
        DoubleBuffered = true; // Reduce flickering
        ResizeRedraw = true;    // Redraw on resize
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        // Background color for health bar
        e.Graphics.Clear(Color.Red);

        // Calculate width of health portion
        int healthWidth = (int)(Width * (health / 100.0));

        // Draw the health bar
        e.Graphics.FillRectangle(Brushes.Green, 0, 0, healthWidth, Height);

        // Draw the health text on top
        string healthText = $"{health}%";
        var textSize = e.Graphics.MeasureString(healthText, Font);
        var textPosition = new PointF((Width - textSize.Width) / 2, (Height - textSize.Height) / 2);

        e.Graphics.DrawString(healthText, Font, Brushes.White, textPosition);
    }
}
