﻿#region Copyright & License

// Copyright © 2020 - 2020 Emmanuel Benitez
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

using System.Linq;
using BlazorComponentUtilities;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Components.Rendering;

namespace BigSolution.Blazor
{
    /// <summary>
    /// Defines a dynamic HTML element
    /// </summary>
    public class DynamicElement : DynamicComponentWithBodyBase
    {
        #region Base Class Member Overrides

        /// <inheritdoc />
        protected override void BuildRenderTree([NotNull] RenderTreeBuilder builder)
        {
            Requires.NotNull(builder, nameof(builder));
            var sequenceGenerator = new SequenceGenerator();

            builder.OpenElement(sequenceGenerator.GetNextValue(), TagName);
            builder.AddAttribute(sequenceGenerator.GetNextValue(), "class", CssClasses);
            builder.AddMultipleAttributes(sequenceGenerator.GetNextValue(), AdditionalAttributes?.Where(pair => pair.Key != "class"));
            builder.AddContent(sequenceGenerator.GetNextValue(), ChildContent);
            builder.CloseElement();
        }

        #endregion

        #region Base Class Member Overrides

        /// <summary>
        /// Gets the CSS builder
        /// </summary>
        protected override CssBuilder CssBuilder => new CssBuilder();

        #endregion
    }
}
