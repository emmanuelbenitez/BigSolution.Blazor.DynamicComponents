#region Copyright & License

// Copyright © 2020 - 2023 Emmanuel Benitez
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

using FluentAssertions;
using Xunit;

namespace BigSolution.Blazor;

public class CssClassBuilderFixture
{
	[Theory]
	[MemberData(nameof(GetValidBuilder))]
	public void BuildSucceeds(CssClassBuilder builder, string expected)
	{
		builder.Build().Should().Be(expected);
	}

	[Theory]
	[InlineData(null)]
	[InlineData("")]
	[InlineData(" ")]
	[InlineData("prefix")]
	public void InstanceCreated(string prefix)
	{
		_ = new CssClassBuilder(prefix);
	}

	[Fact]
	public void ClearSucceeds()
	{
		var builder = new CssClassBuilder()
			.Append("test")
			.Append("test");

		builder.Reset();

		builder.Build().Should().BeEmpty();
	}

	public static IEnumerable<object[]> GetValidBuilder()
	{
		yield return new object[] { new CssClassBuilder("a").Append(string.Empty), "a" };
		yield return new object[] { new CssClassBuilder("a").Append(" "), "a" };
		yield return new object[] { new CssClassBuilder("a").Append("test"), "a-test" };
		yield return new object[] { new CssClassBuilder("a").Append("test", false), "a" };
		yield return new object[] { new CssClassBuilder("a").Append("test", () => false), "a" };
		yield return new object[] { new CssClassBuilder("a").Append("test", () => true), "a-test" };
		yield return new object[] { new CssClassBuilder().Append("test"), "test" };
		yield return new object[] { new CssClassBuilder("a", "_").Append("test"), "a_test" };
	}
}