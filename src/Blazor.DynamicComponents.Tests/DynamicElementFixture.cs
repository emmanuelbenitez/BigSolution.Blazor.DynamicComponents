#region Copyright & License

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

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using Bunit;
using Bunit.Rendering;
using FluentAssertions;
using Xunit;

namespace BigSolution.Blazor
{
    [SuppressMessage("Usage", "BL0005:Component parameter should not be set outside of its component.", Justification = "Test Purpose", Scope = "member")]
    public class DynamicElementFixture : TestContext
    {
        [Fact]
        public void ClassesRendered()
        {
            var renderedComponent = RenderComponent<DynamicElement>(ComponentParameter.CreateParameter(nameof(DynamicElement.Classes), "test"));
            renderedComponent.Find("div").ClassName.Should().Be("test");
        }

        [Fact]
        public void CssClassesWellFormatted()
        {
            new DynamicElement {
                Classes = "test",
                AdditionalAttributes = new ReadOnlyDictionary<string, object>(new Dictionary<string, object> { { "class", "value" } })
            }.CssClasses.Should().Be("test value");
        }

        [Fact]
        public void TagNameRendered()
        {
            var renderedComponent = RenderComponent<DynamicElement>(ComponentParameter.CreateParameter(nameof(DynamicElement.TagName), "test"));
            renderedComponent.Find("test").Should().NotBeNull();
        }
    }
}
