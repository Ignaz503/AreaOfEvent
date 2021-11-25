using System;
using System.Threading.Tasks;
using AreaOfEvent.Client.Components.Rendering.Components;
using AreaOfEvent.Shared.Mathematics;
using Blazor.Extensions.Canvas.Canvas2D;


namespace AreaOfEvent.Client.Components.Rendering
{
    public class TextRenderer : Component, IRenderable
    {
        public string Text { get; set; }
        public string FontInfo { get; set; }

        public TextAlign TextAlignment { get; set; } = TextAlign.Start;

        public TextBaseline Baseline { get; set; } = TextBaseline.Alphabetic;

        public object DrawStyle { get; set; }

        public bool Filled { get; set; } = true;

        protected virtual Vector2 DrawPoistion => Transform.Position;

        protected virtual float DrawRotationRadian => Transform.RotationRadian;

        public TextRenderer( SceneObject containingObject ) : base( containingObject )
        {
        }

        public async Task Render( RenderContext ctx )
        {
            await ctx.CanvasCTX.SaveAsync();

            await ctx.CanvasCTX.SetFontAsync( FontInfo );

            await ctx.CanvasCTX.SetTextAlignAsync( TextAlignment );

            await ctx.CanvasCTX.SetTextBaselineAsync( Baseline );

            //rotate around center of text
            //https://developer.mozilla.org/en-US/docs/Web/API/CanvasRenderingContext2D/rotate
            await ctx.CanvasCTX.TranslateAsync( DrawPoistion.X, DrawPoistion.Y );
            await ctx.CanvasCTX.RotateAsync( DrawRotationRadian );
            await ctx.CanvasCTX.TranslateAsync( -DrawPoistion.X, -DrawPoistion.Y );

            if (Filled)
            {
                await ctx.CanvasCTX.SetFillStyleAsync( DrawStyle );
                await ctx.CanvasCTX.FillTextAsync( Text, DrawPoistion.X, DrawPoistion.Y );
            } else
            {
                if (DrawStyle is not string)
                {
                    Console.WriteLine( $"{SceneObject.Name}: {nameof( TextRenderer )} In Non Filled mode draw style needs to be a string defaulting to value #000000" );
                    await ctx.CanvasCTX.SetStrokeStyleAsync( "#000000" );
                }

                await ctx.CanvasCTX.StrokeTextAsync( Text, DrawPoistion.X, DrawPoistion.Y );
            }

            await ctx.CanvasCTX.RestoreAsync();
        }
    }

}
